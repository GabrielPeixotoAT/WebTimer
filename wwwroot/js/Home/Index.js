function InsertRecord() {
    var dados = {
        Status: parseInt($('#selectStatus').val()),
    };

    var json = JSON.stringify(dados);

    $.ajax({
        type: "POST",
        url: document.URL + "Home/InsertRecord",
        contentType: "application/json",
        data: json,
        success: function (result) {
            if (result) {
                let array = result.split('|');
                if (array[1] != '') {
                    $('#startTime').text('Iniciado às: ' + array[0]);
                    $('#btn-title').text('Parar');
                }
                else {
                    $('#startTime').text('');
                    $('#btn-title').text('Iniciar');
                }
            }
        },
        error: function (errorThrown) {
            Swal.fire({
                icon: 'error',
                title: 'Erro',
                text: errorThrown.responseText
            })
        }
    });
};
