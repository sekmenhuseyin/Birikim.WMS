using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace Wms12m.Entity.Models
{
	internal static class DatabaseUtils
	{
		/// <summary>
		/// Builds the connection string for Entity framework.
		/// </summary>
		/// <returns></returns>
		public static EntityConnection BuildConnection(NewConnectionParams buildConnectionParams)
		{
			var sqlBuilder = new SqlConnectionStringBuilder
			{
				DataSource = buildConnectionParams.ServerName,
				InitialCatalog = buildConnectionParams.DatabaseName,             
				UserID = buildConnectionParams.UserID,
				Password= buildConnectionParams.Password
				//IntegratedSecurity = true,
			};

			var providerString = sqlBuilder.ToString();
			var entityBuilder = new EntityConnectionStringBuilder
			{
				Provider = buildConnectionParams.ProviderName,
				ProviderConnectionString = providerString,
				Metadata = string.Format(@"res://*/{0}.csdl|
							res://*/{0}.ssdl|
							res://*/{0}.msl", buildConnectionParams.ModelName)
			};

			return CreateConnection(buildConnectionParams.SchemaName, entityBuilder, buildConnectionParams.ModelName);
		}

		/// <summary>
		/// Creates the EntityConnection, based on new schema & existing connectionString
		/// </summary>
		private static EntityConnection CreateConnection(string schemaName, EntityConnectionStringBuilder connectionBuilder, string modelName)
		{
			Func<string, Stream> generateStream =
				extension => Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Concat(modelName, extension));

			Action<IEnumerable<Stream>> disposeCollection = streams =>
			{
				if (streams == null)
					return;

				foreach (var stream in streams.Where(stream => stream != null))
					stream.Dispose();
			};

			var conceptualReader = generateStream(".csdl");
			var mappingReader = generateStream(".msl");
			var storageReader = generateStream(".ssdl");

			if (conceptualReader == null || mappingReader == null || storageReader == null)
			{
				disposeCollection(new[] { conceptualReader, mappingReader, storageReader });
				return null;
			}

			var storageXml = XElement.Load(storageReader);

			foreach (var entitySet in storageXml.Descendants())
			{
				var schemaAttribute = entitySet.Attributes("Schema").FirstOrDefault();
				if (schemaAttribute != null)
					schemaAttribute.SetValue(schemaName);
			}

			storageXml.CreateReader();

			var workspace = new MetadataWorkspace();

			var storageCollection = new StoreItemCollection(new[] { storageXml.CreateReader() });
			var conceptualCollection = new EdmItemCollection(new[] { XmlReader.Create(conceptualReader) });
			var mappingCollection = new StorageMappingItemCollection(conceptualCollection,
																	storageCollection,
																	new[] { XmlReader.Create(mappingReader) });

			workspace.RegisterItemCollection(conceptualCollection);
			workspace.RegisterItemCollection(storageCollection);
			workspace.RegisterItemCollection(mappingCollection);

			var connection = DbProviderFactories.GetFactory(connectionBuilder.Provider).CreateConnection();
			if (connection == null)
			{
				disposeCollection(new[] { conceptualReader, mappingReader, storageReader });
				return null;
			}

			connection.ConnectionString = connectionBuilder.ProviderConnectionString;
			return new EntityConnection(workspace, connection);
		}
	}


	public class DinamikModelContext : IDisposable
	{
		private readonly string EDMXModelName = "Models.ModelFinsat"; // EDMX MODELNAME
		public FINSATEntities Context;

		public DinamikModelContext(string SirketKodu)
		{
			var entityConnection = DatabaseUtils.BuildConnection(new NewConnectionParams
			{
				ProviderName = "System.Data.SqlClient",
				ServerName = "TESTSERVER",
				DatabaseName = string.Format("FINSAT6{0}", SirketKodu),
				ModelName = EDMXModelName,
				SchemaName = string.Format("FINSAT6{0}", SirketKodu),
				UserID = "sa",
				Password = "Birikim12"
			});

			if (entityConnection == null)
				throw new Exception("EntityConnection oluşturulamadı !!");

			Context = new FINSATEntities(entityConnection);
		}


		


		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (Context == null)
				return;

			Context.Dispose();
			Context = null;
		}
	}


	internal struct NewConnectionParams
	{
		public string ProviderName
		{
			get;
			set;
		}

		public string ServerName
		{
			get;
			set;
		}

		public string DatabaseName
		{
			get;
			set;
		}

		public string UserID
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}


		public string ModelName
		{
			get;
			set;
		}

		public string SchemaName
		{
			get;
			set;
		}
	}

	// public FinsatContext(EntityConnection entityConnection) : 
	//                       base(entityConnection, false)
	//{
	//}
}
