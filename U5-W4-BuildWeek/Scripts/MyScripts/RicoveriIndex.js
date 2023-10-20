function filtra() {

    /*$("#tab").empty()*/
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
                            "<div class='card' style=''>" +
                        "<img src ='./Content/Imgs/Animali/" + item.Foto + "' class='card-img-top w-100' alt = '..' style='height: 18rem; object-fit: cover;'>" +
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

    //$("#agg").empty()
    $("#tab").removeClass("d-none")
    //$("#tab").empty()

    //Faccio la chiamata asincrona
    $.ajax
        ({
            method: 'GET',
            url: "Ricoveri/EstraiRicoveri",
            success: function (animali) {
                $("#tabdati").empty()

                let thead = `
                <tr>
                    <th>Nome</th>
                    <th>Inizio Ricovero</th>
                    <th>Giorni trascorsi</th>
                    <th>Data nascita presunta</th>
                    <th>MicroChip</th>
                    <th>Tipo</th>
                </tr>`

                $("#tabdati").append(thead)
                $.each(animali, function (i, item) {
                    let giorniTrascorsi = calcolaGiorniTrascorsi(item.DataInizioRicovero)
                    let dataInizioRicovero = convertiData(item.DataInizioRicovero);

                    let DataDiNascita;
                    if (item.DataNascita && item.DataNascita !== "-") {
                        DataDiNascita = convertiData(item.DataNascita);
                    } else {
                        DataDiNascita = "-";
                    }

                    let tds = `
                    <tr>
                        <td>
                            <p>${item.Nome}</p>
                        </td>
                        <td>
                            <p>${dataInizioRicovero.toLocaleDateString()}</p>
                        </td>
                        <td>
                            <p>${giorniTrascorsi}</p>
                        </td>
                        <td>
                            <p>${DataDiNascita instanceof Date ? DataDiNascita.toLocaleDateString() : DataDiNascita}</p>
                        </td>
                        <td>
                            <p>${item.Microchip ? item.Microchip : "-"}</p>
                        </td>
                        <td>
                            <p>${item.Tipologia}</p>
                        </td>
                    </tr >`

                    $("#tabdati").append(tds)
                })
            },
            error: function (e) {
                console.log(e)
            }
        })
}

function convertiData(data) {

    let dataEora = data.split(' ');
    //["14/10/2023", "00:00:00"]//

    let giornoMeseAnno = dataEora[0].split('/');
    //["14", "10", "2023"]//

    // Formato Javascript: YYYY-MM-DDTHH:mm:ssZ
    let dataJs = giornoMeseAnno[2] + '-' + giornoMeseAnno[1] + '-' + giornoMeseAnno[0] + 'T' + dataEora[1] + 'Z';

    return new Date(dataJs);
}

function calcolaGiorniTrascorsi(dataInizio) {
    let dataInizioRicovero = convertiData(dataInizio);
    let oggi = new Date();
    //Thu Oct 19 2023 12:52:34 GMT+0200

    // (oggi - dataInizioRicovero) -> il risultato è la differenza tra le due date in millisecondi
    // 1000 millisecondi in un secondo, 60 secondi in un minuto, 60 minuti in un’ora e 24 ore in un giorno
    // moltiplicando tutti questi insieme otteniamo il numero di millisecondi in un giorno
    return Math.floor((oggi - dataInizioRicovero) / (1000 * 60 * 60 * 24));
}