$('.products-slider').slick({
    slidesToShow: 3,
    slidesToScroll: 2,
    speed: 300,
    autoplaySpeed: 2000,
    responsive: [
        {
            breakpoint: 1024,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
                infinite: true,
                dots: true
            }
        },
        {
            breakpoint: 600,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2
            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }
    ]
});

$('.news-slider').slick({
    slidesToShow: 3,
    slidesToScroll: 3,
    speed: 300,
    responsive: [
        {
            breakpoint: 1200,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3,
                infinite: true,
                dots: true
            }
        },
        {
            breakpoint: 1024,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
                infinite: true,
                dots: true
            }
        },
        {
            breakpoint: 768,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }
    ]
});

$('.employer-slider').slick({
    infinite: true,
    autoplay: false,
    slidesToShow: 5,
    slidesToScroll: 1,
    speed: 300,
    autoplaySpeed: 2000,
    responsive: [
        {
            breakpoint: 1024,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3,
                infinite: true,
                dots: true
            }
        },
        {
            breakpoint: 600,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2
            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }
    ]
});

const initPortfolio = () => {
    $(".portfolio-grid").isotope({
        filter: $(this).attr('data-filter'),
    });


    $('.filters-button-group').on('click', 'button', function () {
        $(".portfolio-grid").isotope({
            filter: $(this).attr('data-filter'),
        });
    });

    $('.button-group').each(function (i, buttonGroup) {
        const $buttonGroup = $(buttonGroup);
        $buttonGroup.on('click', 'button', function () {
            $buttonGroup.find('.is-checked').removeClass('is-checked');
            $(this).addClass('is-checked');
        });
    });
}

const scrollToTopButton = () => {
    // Get the button:
    const btn = document.getElementById("scrollToTop");

    const scrollFunction = () => {
        if (document.body.scrollTop > 250 || document.documentElement.scrollTop > 250) {
            btn.style.display = "flex";
        } else {
            btn.style.display = "none";
        }
    }

    window.onscroll = function () { scrollFunction() };
}

window.addEventListener('load', (event) => {
    const val1 = new countUp.CountUp(document.getElementById('val1'), document.getElementById('val1')?.getAttribute('data-value'));
    const val2 = new countUp.CountUp(document.getElementById('val2'), document.getElementById('val2')?.getAttribute('data-value'));
    const val3 = new countUp.CountUp(document.getElementById('val3'), document.getElementById('val3')?.getAttribute('data-value'));
    const val4 = new countUp.CountUp(document.getElementById('val4'), document.getElementById('val4')?.getAttribute('data-value'));

    new Waypoint({
        element: document.getElementById('products'),
        handler: function (direction) {
            if (direction === 'down') {
                val1.start();
                val2.start();
                val3.start();
                val4.start();
            }
        }
    });

    initPortfolio();
    scrollToTopButton();
});

