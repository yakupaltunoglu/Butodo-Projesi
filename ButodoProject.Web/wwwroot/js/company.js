
$(function () {
    console.log("ready!");
    // form-send
    $('.form-submit').click(function (e) {
        $('#myModal').modal('hide')
        e.preventDefault();

        //if (!instance.isValid())
        //    return;

        var button = $(this);

        var formid = $(this).data('formid');
        if (formid == undefined) {
            var form = $(this).parents('form');
            formid = form.attr('id');
        }

        if (formid == undefined) {
            alert('Form Bulunamadı!');
            return;
        }

        var url = $(this).data('url');
        var action = $(this).data('action');
        $('.form-submit').button('loading');

        var control = formValidation(formid);
        if (!control) {
            $('.form-submit').button('reset');
            return;
        }

        var postData = getFormData($('#' + formid));
        //postData.FvdItems = fvdGetData();
        console.log(postData)
        $.ajax({
            url: url,
            type: 'post',
            dataType: 'json',
            data: postData,
            success: function (response) {
                $('#exampleModal').modal('hide');
                


                console.log(response)
                //if (!response.success) {
                //    alertify.error(response.Message);
                //    $('.form-submit').button('reset');
                //} else {
                //    console.log(action)
                //    console.log(button.data('actiontype'))
                //    if (action == 'reload') {
                //        if (button.data('actiontype') == "edit") {
                //            //location.reload();
                //            location.href = response.Data;
                //        } else {
                //            location.href = response.Data;
                //        }
                //    }
                //    else if (action == 'message') {
                //        alertify.success(response.Message);
                //        $('.form-submit').button('reset');
                //    }
                //    else if (action == 'backlist') {
                //        location.href = "/cms/";
                //    }
                //    else {
                //        location.href = action;
                //    }
                //}

            },
            error: function () {
                alert("Beklenmeyen bir hata oluþtu!");
                $('.form-submit').button('reset');
            }
        });
    });
});

function formValidation(formId) {
    var form = $("#" + formId);
    $("#" + formId + " .error").removeClass('error');

    var Inputs = form.find(":input");
    IsValid = true;
    Id = "";
    mailpattern = /^\s*[\w\-\+_]+(\.[\w\-\+_]+)*\@[\w\-\+_]+\.[\w\-\+_]+(\.[\w\-\+_]+)*\s*$/;

    Inputs.each(function (index, element) {
        Id = element.id;
        if (Id == "" || Id == undefined) return true;
        /*
        console.log(Id)
        if ($("#" + Id).length == 0) {
            console.log(element)
            return true;
        }
        */
        if ($("#" + Id).hasClass("notRequire")) {
            return true;
        }

        if (Id == "g-recaptcha-response") {
            return true;
        }

        var name = $("#" + Id).attr('Header');

        if (element.value == "" || ($("#" + Id).attr('type') == "checkbox") && $("input[Header=" + name + "]:checked").val() == undefined) {
            IsValid = false;

            if ($("#" + Id).attr('type') == "checkbox") {
                $("#" + formId + " #" + Id).parent().addClass("error");
                //$(this).parent().closest(".form-group").find("span.help-block").text(validMessages[messageIndex]);
            }
            else if ($("#" + Id).attr('type') == "radio") {
                $("#" + formId + " #" + Id).parent().addClass("error");
            }
            else if ($("#" + Id).attr('rel') == "Image") {
                $("#" + formId + " #" + Id + '-image').parent().addClass("error");
                //$("#" + Id + '-image').parent().find("span.help-block").text(validMessages[messageIndex]);
            } else {
                $("#" + formId + " #" + Id).addClass("error");
                //$("#" + formId + " #" + Id).after('<div class="help-inline"><i></i><div><p> Zorunlı Alan</p></div></div>');

            }

        }
        else if ($("#" + Id).attr('type') == "radio") {
            var rname = $("#" + Id).attr('name');
            var valu = $("input[name=" + rname + "]:checked").val();
            if (valu == "" || valu == undefined) {
                $("#" + formId + " #" + Id).parent().parent().addClass("error");
                //$("#" + formId + " #" + Id).parent().parent().append('<div class="help-inline"><i></i> <div><p>Cinsiyet seçiniz.</p></div></div>');
                //$("#" + formId + " #" + Id + " .help-inline").fadeIn("slow");

            }
        }

        if ($("#" + Id).attr('type') == "email" || $("#" + Id).data('format') === "email") {
            if (!mailpattern.test(element.value) && element.value != "") {
                IsValid = false;
                $("#" + formId + " #" + Id).addClass("error");
                //$("#" + formId + " #" + Id).after('<div class="help-inline"><i></i><div><p> Geçerli e-posta<br>adresi giriniz.</p></div></div>');
            }
        }

        if ($("#" + Id).data('format') === "date") {

            if (!isValidDate($('#BirthDate').val())) {
                IsValid = false;

                //$("#" + formId + " #" + Id).siblings('.help-inline').remove();
                $("#" + formId + " #" + Id).parent().addClass("error");
                //$("#" + formId + " #" + Id).after('<div class="help-inline"><i></i><div><p> Geçerli bir tarih giriniz.</p></div></div>');
            }
        }
    });
    //$("#" + formId + " .help-inline").animate({ right: -104, opacity: 1 });

    console.log(Id)
    console.log(IsValid)
    return IsValid;
};
function getFormData($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}




deleteData = form => {
    if (confirm('Bu kayıdı silmek istediğinize emin misiniz ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    var toastElList = [].slice.call(document.querySelectorAll('.toast'))
                    var toastList = toastElList.map(function (toastEl) {
                        return new bootstrap.Toast(toastEl)
                    })
                    toastList.forEach(toast => toast.show())
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}

showDataList = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            fillTableData(res, url);
        }
    })
}   

fillTableData = (res,url) => {
    $("#company-table tbody tr").remove();
    $(res).each(function (i, element) {
        row = "<tr><td>"
            + element.name + "</td></tr>";
        $("#company-table tbody").append(row);
    });
}

addData = form => {
    $.ajax({
        async: false,
        type: 'POST',
        url: form.action,
        data: new FormData(form),
        contentType: false,
        processData: false,
        success: function (res) {
            $('#success').html("başarılı");
            showDataList("company/GetCompanyList")
        },
        error: function (err) {
            //console.log(err)
            $('#error').html(err);
        }
    })
}