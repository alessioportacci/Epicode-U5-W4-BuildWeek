function filtra()
{

    //Mi prendo le checkbox
    let lista = []
    $(':checkbox').each(function () {
        if (this.checked)
            lista.push($(this).attr("id"))
    });

    //Faccio la chiamata asincrona
    $.ajax
        ({
            method: 'POST',
            url: "Ricoveri/RicoveriFilter",
            data:
            {
                Tipologie: lista
            },
            success: function (animali) {
                $("#agg").empty()
                $.each(animali, function (i, item) {
                    let card =
                        "<div class='card' style='width: 18rem;'>" +
                        "<img src = '...' class='card-img-top' alt = '..'>" +
                        "<div class='card-body'> " +
                        "<h5 class='card-title'>" + item.Nome + "</h5> " +
                        "<p class='card-text'>" + item.Colore + "</p>" +
                        "<a href='Ricoveri/Details/" + item.Id + "' class='btn btn-primary'>Go somewhere</a> " +
                        "</div>" +
                        "</div>"


                    $("#agg").append(card)
                })
            },
            error: function (e) {
                console.log(e)
            }
        })
}