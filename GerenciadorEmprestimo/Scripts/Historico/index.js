
$(document).ready(function () {
    grid.carregar();
});

var tabela;
var grid = {
    carregar: function () {
        var filterValues = {};
        tabela = $('#tblHistorico').DataTable({
            ajax: function (data, callback, settings) {
                filterValues.draw = data.draw;
                filterValues.start = data.start;
                filterValues.length = data.length;
                $.ajax({
                    url: ROOT_URL + "api/Historico/Consultar",
                    method: 'GET',
                    data: filterValues
                }).done(callback);
            },
            "language": {
                "lengthMenu": "Exibir _MENU_ registros por pagina",
                "zeroRecords": "Nenhum resultado encontrado",
                "info": "Exibindo página _PAGE_ de _PAGES_",
                "infoEmpty": "Nenhum registro encontrado",
                "infoFiltered": "(filtrado de um total de _MAX_ registros)",
                sSearch: "Pesquisar:",
                sLengthMenu: "Exibir _MENU_ registros",
                sInfo: "Exibindo _START_ até _END_ de _TOTAL_ registros",
                sNext: "Próximo",
                sPrevious: "Anterior"
            },

            "columns": [
                { data: "Jogo" },
                { data: "Amigo" },
                { data: "DataInicio" },
                 { data: "DataFim" }
            ]
        });
    },
    atualizar: function () {
        tabela.ajax.reload();
    }
}
