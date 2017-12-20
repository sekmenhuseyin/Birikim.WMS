////////////////////////////////////////////////////////////////////////////////////
//https://github.com/Chalarangelo/30-seconds-of-code
////////////////////////////////////////////////////////////////////////////////////
// isemail(sekmenhuseyin@gmail.com) -> true
const validateEmail = str => /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(str);
// capitalizeEveryWord('hello world!') -> 'Hello World!'
const capitalizeEveryWord = str => str.replace(/\b[a-z]/g, char => char.toUpperCase());
// capitalize('myName', true) -> 'Myname'
const capitalize = (str, lowerRest = false) => str.slice(0, 1).toUpperCase() + (lowerRest ? str.slice(1).toLowerCase() : str.slice(1));
// currentUrl() -> 'https://google.com'
const currentUrl = _ => window.location.href;
// isEven(3) -> false
const isEven = num => Math.abs(num) % 2 === 0;
// arrayMax([10, 1, 5]) -> 10
const arrayMax = arr => Math.max(...arr);
// arrayMin([10, 1, 5]) -> 1
const arrayMin = arr => Math.min(...arr);
// randomizeOrder([1,2,3]) -> [1,3,2]
const randomizeOrder = arr => arr.sort((a, b) => Math.random() >= 0.5 ? -1 : 1);
// redirect('https://google.com')
const redirect = (url, asLink = true) => asLink ? window.location.href = url : window.location.replace(url);
// rgbToHex(255, 165, 1) -> 'ffa501'
const rgbToHex = (r, g, b) => ((r << 16) + (g << 8) + b).toString(16).padStart(6, '0');
// scrollToTop()
const scrollToTop = _ =>
{
    const c = document.documentElement.scrollTop || document.body.scrollTop;
    if (c > 0)
    {
        window.requestAnimationFrame(scrollToTop);
        window.scrollTo(0, c - c / 8);
    }
};
// shuffle([1,2,3]) -> [2, 1, 3]
const shuffle = arr =>
{
    let r = arr.map(Math.random);
    return arr.sort((a, b) => r[a] - r[b]);
};
// truncate('boomerang', 7) -> 'boom...'
const truncate = (str, num) => str.length > num ? str.slice(0, num > 3 ? num - 3 : num) + '...' : str;
// getUrlParameters('http://url.com/page?name=Adam&surname=Smith') -> {name: 'Adam', surname: 'Smith'}
const getUrlParameters = url =>
    url.match(/([^?=&]+)(=([^&]*))?/g).reduce(
        (a, v) => (a[v.slice(0, v.indexOf('='))] = v.slice(v.indexOf('=') + 1), a), {}
    );
// uuid() -> '7982fcfe-5721-4632-bede-6000885be57d'
const uuid = _ =>
    ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
// validateNumber('10') -> true
const validateNumber = n => !isNaN(parseFloat(n)) && isFinite(n);