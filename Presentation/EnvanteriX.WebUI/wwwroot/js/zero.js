$("#multi-filter-select").DataTable({
    pageLength: 5,
    order: [],
    initComplete: function () {
        this.api()
            .columns()
            .every(function () {
                var column = this;
                var select = $(
                    '<select class="form-select"><option value=""></option></select>'
                )
                    .appendTo($(column.footer()).empty())
                    .on("change", function () {
                        var val = $.fn.dataTable.util.escapeRegex($(this).val());

                        column
                            .search(val ? "^" + val + "$" : "", true, false)
                            .draw();
                    });

                column
                    .data()
                    //.unique()
                    //.sort()
                    .each(function (d, j) {
                        select.append(
                            '<option value="' + d + '">' + d + "</option>"
                        );
                    });
            });
    },
});
WebFont.load({
    google: { families: ["Public Sans:300,400,500,600,700"] },
    custom: {
        families: [
            "Font Awesome 5 Solid",
            "Font Awesome 5 Regular",
            "Font Awesome 5 Brands",
            "simple-line-icons",
        ],
        urls: [window.location.origin + "/css/fonts.min.css"],
    },
    active: function () {
        sessionStorage.fonts = true;
    },
});


//loading
// Global Loading Fonksiyonları
function showLoading() {
    document.getElementById('loadingOverlay').classList.add('active');
}

function hideLoading() {
    document.getElementById('loadingOverlay').classList.remove('active');
}

// Sayfa yüklendiğinde loading'i gizle
window.addEventListener('load', function () {
    hideLoading();
});
// Navigate fonksiyonu - button'lar için
function navigateWithLoading(url) {
    showLoading();
    window.location.href = url;
}
// Sayfa geri geldiğinde loading'i gizle (browser back button)
window.addEventListener('pageshow', function (event) {
    hideLoading();
});

// DOM hazır olduğunda
$(document).ready(function () {
    // Tüm formları yakala
    $('form').on('submit', function (e) {
        // Form validation kontrolü (varsa)
        var form = $(this)[0];
        if (form.checkValidity && !form.checkValidity()) {
            return; // Form geçersizse loading gösterme
        }
        showLoading();
    });

    // AJAX istekleri için
    $(document).ajaxStart(function () {
        showLoading();
    }).ajaxStop(function () {
        hideLoading();
    }).ajaxError(function () {
        hideLoading();
    });

    // Tüm linklere tıklandığında (isteğe bağlı)
    $('a:not([target="_blank"])').on('click', function (e) {
        var href = $(this).attr('href');
        if (href && href !== '#' && !href.startsWith('javascript:')) {
            showLoading();
        }
    });

});

///loading sonu

function goBack() {
    showLoading();
    // Önce TempData mesajlarını temizle
    fetch('/Home/ClearTempMessages', { method: 'POST' })
        .then(response => response.json())
        .then(data => {
            // Backend'den dönen PreviousUrl'i kullan
            const previousUrl = data.previousUrl;
            if (previousUrl) {
                window.location.href = previousUrl;
            } else {
                // Eğer PreviousUrl yoksa browser'ın geri butonunu kullan
                window.history.back();
            }
        })
        .catch(() => {
            // Hata durumunda browser'ın geri butonunu kullan
            window.history.back();
        });
}

//document.addEventListener("DOMContentLoaded", function () {
//    var buttons = document.querySelectorAll(".confirm-btn");
//    buttons.forEach(function (btn) {
//        btn.addEventListener("click", function (e) {
//            e.preventDefault();
//            var url = btn.getAttribute("data-url");

//            swal({
//                title: "Emin misiniz?",
//                text: "Bu işlemi geri alamazsınız!",
//                icon: "warning",
//                buttons: ["İptal", "Evet, devam et"],
//                dangerMode: true
//            }).then(function (willDelete) {
//                if (willDelete) {
//                    window.location.href = url;
//                }
//            });
//        });
//    });
//});
$(document).ready(function () {
    // Sayfa yüklendiğinde, ve her sayfa geçişinde aşağıdaki kod çalışacak
    $(document).on("click", ".confirm-btn", function (e) {
        e.preventDefault();
        var url = $(this).data("url");  // jQuery ile data-url'yi alıyoruz

        swal({
            title: "Emin misiniz?",
            text: "Bu işlemi geri alamazsınız!",
            icon: "warning",
            buttons: ["İptal", "Evet, devam et"],
            dangerMode: true
        }).then(function (willDelete) {
            if (willDelete) {
                showLoading();
                window.location.href = url;  // Kullanıcı silmeyi onaylarsa yönlendirme yapıyoruz
            }
        });
    });
});


$(document).ready(function () {
    $('.select2').select2({
        theme: 'bootstrap', // istersen bootstrap-5 veya default
        width: '100%'
    });
});