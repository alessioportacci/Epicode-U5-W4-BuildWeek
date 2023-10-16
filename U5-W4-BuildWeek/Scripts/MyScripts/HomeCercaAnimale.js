function cerca(microchip)
{
    $.ajax
        ({
            method: 'POST',
            url: "CercaAnimaleByChip",
            data:
            {
                Chip: microchip
            },
            success: function (risultato)
            {
                if (risultato == 0)
                    $("#not-found").removeClass("d-none")
                else
                    //TODO: Aggiungere il link della pagina corretto
                    window.open("../Animali/DettaglioAnimale/" + risultato)
            },
            error: function (e) {
                console.log(e)
            }
        })
}




$("#cerca").click(function ()
{
    cerca($("#Microchip").val())
})