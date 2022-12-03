
$(document).ready(function () {

    // Удалить Спиннер после полной загрузки страницы
    //<div class="preloader"><i class="fa fa-sun-o fa-spin"></i></div>
    $('.preloader').remove();
    $('.preloader-manager').show();

    // Setting to change Value attribute
    $(":checkbox").change(function () {
        if (this.checked) {
            $(this).val(true);
        }
        else {
            $(this).val(false);
        }
    });
    // Setting to change Value attribute
    $("input[type='date']").change(function () {
        var d = this.value;
        console.log(this);
        console.log(d);
        $(this).attr("value", d);
    });

    $('#data-table').DataTable({
        paging: true,
        language: {
            url: '//cdn.datatables.net/plug-ins/1.12.1/i18n/ru.json'
        }
    });
});

// Показ изображения в полном размере
//<div class="show-image" onclick="HideImage()"><img id="showed-image" src="" class="rounded mx-auto d-block" alt=""></div>
function ShowImage(element) {

    const source = element.getAttribute("src");
    const alted = element.getAttribute("alt");
    $("#showed-image").attr("src", source);
    $("#showed-image").attr("alt", alted);
    $(".show-image").css("display", "flex");
}

function HideImage() {
    $(".show-image").css("display", "none");
}

