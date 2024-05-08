AOS.init({
    once: true,
});

window.addEventListener('load', (event) => {
    // new Waypoint({
    //     element: document.getElementById('customers'),
    //     handler: function (direction) {
    //         const navbar = document.getElementById('navbar');
    //         if (direction === 'down') {
    //             navbar.classList.add('fixed-top');
    //         } else {
    //             navbar.classList.remove('fixed-top');
    //         }
    //     }
    // });

    $(".preloader").fadeOut(() => {
        $('body').css("overflow", "auto");
    });
});


window.Notify = function (text, type) {
    var notyObj = noty({
        text: text,
        type: type,
        dismissQueue: true,
        timeout: 5000,
        layout: "bottomLeft",
        closeWith: ["click"],
        maxVisible: 10,
        theme: "flat"
    });
    return notyObj;
}

const setTheme = (name) => {
    document.body.classList.remove(...['theme-middle-blue', 'theme-panton', 'theme-persian-green', 'theme-blue-cola', 'theme-keremi']);
    document.body.classList.add(...[name]);
}


function changeLang(elm) {
    document.getElementById('bootstraplink').remove();

    const lang = elm.getAttribute('data-lang');

    const html = document.getElementsByTagName('html')[0];
    const link = document.createElement('link');
    link.setAttribute('id', 'bootstraplink');
    link.setAttribute('rel', 'stylesheet');

    switch (lang) {
        case 'en-US': {
            link.setAttribute('href', 'assets/css/bootstrap-5.2.0/css/bootstrap.min.css');
            html.setAttribute('dir', 'ltr');

            elm.innerText = 'FA';
            elm.setAttribute('data-lang', 'fa-IR');

            break;
        }
        case 'fa-IR': {
            link.setAttribute('href', 'assets/css/bootstrap-5.2.0/css/bootstrap.rtl.min.css');
            html.setAttribute('dir', 'rtl');

            elm.innerText = 'EN';
            elm.setAttribute('data-lang', 'en-US');

            break;
        }
    }

    document.head.prepend(link);
}