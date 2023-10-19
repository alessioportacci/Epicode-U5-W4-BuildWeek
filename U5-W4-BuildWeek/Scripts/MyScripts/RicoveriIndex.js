function filtra()
{

    $("#tab").empty()
    $("#main-tab").addClass("d-none")


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
                        "<div class='p-2 px-3'>" +
                            "<div class='card' style='width:'>" +
                                "<img src ='./Content/Imgs/Animali/" + item.Foto + "' class='card-img-top w-100' alt = '..'>" +
                                "<div class='card-body'> " +
                                    "<h5 class='card-title'>" + item.Nome + "</h5> " +
                                    "<p class='card-text'>" + item.Colore + "</p>" +
                                    "<a href='Animali/DettaglioAnimale/" + item.Id + "' class='btn btn-outline-dark m-auto'>Storia medica</a> " +
                                "</div>" +
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


function RicoveriList() {

    $("#agg").empty()
    $("#main-tab").removeClass("d-none")
    $("#tab").empty()


    $("#tab").append("<tr>" +
        "<th> Nome </th>" +
        "<th> Inizio Ricovero </th>" +
        "<th> Data nascita presunta </th>" +
        "<th> MicroChip </th>" +
        "<th> Razza </th>" +
    "</tr>")

    //Faccio la chiamata asincrona
    $.ajax
        ({
            method: 'GET',
            url: "Ricoveri/EstraiRicoveri",
            success: function (animali) {
                $("#agg").empty()
                $.each(animali, function (i, item) {
                    let tds =
                        "<tr>" +
                            "<th>" + item.Nome + "</th>" +
                            "<th>" + item.DataInizioRicovero+ "</th>" +
                            "<th>" + item.DataNascita + "</th>" +
                            "<th>" + item.Microchip + "</th>" +
                            "<th>" + item.Tipologia + "</th>" +
                        "</tr>"

                    $("#tab").append(tds)
                })
            },
            error: function (e) {
                console.log(e)
            }
        })
}