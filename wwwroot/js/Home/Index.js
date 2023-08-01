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
                $('#startTime').text(result);
            }
        },
        error: function (errorThrown) {
            Swal.fire({
                icon: 'error',
                title: 'Erro',
                text: JSON.stringify(errorThrown)
            })
        }
    });
};
