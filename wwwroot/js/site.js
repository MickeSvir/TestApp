function TestingOrder(provaiderId, number) {
    var dat = "&pid=" + provaiderId + "&number=" + number;
    $.ajax({
        url: '/?testorder',
        method: 'get',
        dataType: 'html',
        async: false,
        data: dat,
        success: function (data) {
            if (data == "true") {
                $('#submitBtn').click();
            }
            else {
                alert("У поставщика '" + $('#ProviderId').val() + "' уже имеется заказ с номером '" + number + "'");
            }
        },
        error: function (jqXHR, exception) {
            if (jqXHR.status === 0) {
                alert('Not connect. Verify Network.');
            } else if (jqXHR.status == 404) {
                alert('Requested page not found (404).');
            } else if (jqXHR.status == 500) {
                alert('Internal Server Error (500).');
            } else if (exception === 'parsererror') {
                alert('Requested JSON parse failed.');
            } else if (exception === 'timeout') {
                alert('Time out error.');
            } else if (exception === 'abort') {
                alert('Ajax request aborted.');
            } else {
                alert('Uncaught Error. ' + jqXHR.responseText);
            }
        }
    });
};

function Delete(num, id) {
    if (confirm("Удалить заказ с номером '" + num + "' ?")) {
        var dat = "&id=" + id;
        $.ajax({
            url: '/?remove',
            method: 'get',
            dataType: 'html',
            async: false,
            data: dat,
            success: function (data) {
                if (data == "true") {
                    alert("Заказ с номером '" + num + "' успешно удалён.");
                    window.location.replace("/Home/Index");
                } else {
                    alert("Внутренняя ошибка :(");
                };
            },
            error: function (jqXHR, exception) {
                if (jqXHR.status === 0) {
                    alert('Not connect. Verify Network.');
                } else if (jqXHR.status == 404) {
                    alert('Requested page not found (404).');
                } else if (jqXHR.status == 500) {
                    alert('Internal Server Error (500).');
                } else if (exception === 'parsererror') {
                    alert('Requested JSON parse failed.');
                } else if (exception === 'timeout') {
                    alert('Time out error.');
                } else if (exception === 'abort') {
                    alert('Ajax request aborted.');
                } else {
                    alert('Uncaught Error. ' + jqXHR.responseText);
                }
            }

        });
    };
};

function RemOrderItem(id) {
    var dat = "&id=" + id;
    $.ajax({
        url: '/?remorderitem',
        method: 'get',
        dataType: 'html',
        async: false,
        data: dat,
        success: function (data) {
            if (data == "true") {
                location.reload(true);
            }
            else {
                alert("Внутренняя ошибка :(");
            }
        },
        error: function (jqXHR, exception) {
            if (jqXHR.status === 0) {
                alert('Not connect. Verify Network.');
            } else if (jqXHR.status == 404) {
                alert('Requested page not found (404).');
            } else if (jqXHR.status == 500) {
                alert('Internal Server Error (500).');
            } else if (exception === 'parsererror') {
                alert('Requested JSON parse failed.');
            } else if (exception === 'timeout') {
                alert('Time out error.');
            } else if (exception === 'abort') {
                alert('Ajax request aborted.');
            } else {
                alert('Uncaught Error. ' + jqXHR.responseText);
            }
        }
    });
};

function AddOrderItem(name, quan, unit) {
    var dat = "&id=" + $('#Id').val() + "&name=" + name + "&quantity=" + quan + "&unit=" + unit;
    var Num = $('#Number').val();
    if (Num == name) {
        alert("Название элемента заказа не может совпадать с номером заказа.");
        return;
    }
    $.ajax({
        url: '/?addorderitem',
        method: 'get',
        dataType: 'html',
        async: false,
        data: dat,
        success: function (data) {
            if (data == "true") {
                location.reload(true);
            }
            else {
                alert("Внутренняя ошибка :(");
            }
        },
        error: function (jqXHR, exception) {
            if (jqXHR.status === 0) {
                alert('Not connect. Verify Network.');
            } else if (jqXHR.status == 404) {
                alert('Requested page not found (404).');
            } else if (jqXHR.status == 500) {
                alert('Internal Server Error (500).');
            } else if (exception === 'parsererror') {
                alert('Requested JSON parse failed.');
            } else if (exception === 'timeout') {
                alert('Time out error.');
            } else if (exception === 'abort') {
                alert('Ajax request aborted.');
            } else {
                alert('Uncaught Error. ' + jqXHR.responseText);
            }
        }
    });
};