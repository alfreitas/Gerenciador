
$(document).ready(function () {
    modal.criarModal();
    modal.eventos();
    grid.carregar();
});

var tabela;
var grid = {
    carregar: function () {
        var filterValues = {};
        tabela = $('#tblPessoa').DataTable({
            ajax: function (data, callback, settings) {
                filterValues.draw = data.draw;
                filterValues.start = data.start;
                filterValues.length = data.length;
                $.ajax({
                    url: ROOT_URL + "api/Pessoa/Consultar",
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
                { data: "Nome" },
                { data: "Endereco" },
                { data: "Cidade" },
                {
                    mData: null, orderable: false, title: 'Ação', sWidth: '7%', mRender: function (objeto) {
                        return '<a href="javascript:void(0);" class="js-alterar"  onclick="modal.abrirModal(\'' + objeto.Nome + '\',\'' + objeto.Endereco + '\',\'' + objeto.Cidade + '\',\'' + objeto.Codigo + '\');" >'
                            + '<img src="' + ROOT_URL + 'Content/img/editar.png" height="20" width="20" alt="Alterar" title="Alterar" class="cursor-pointer"></a> &nbsp;'
                            + '<a href="javascript:void(0);" class="js-excluir" onclick="grid.excluir(' + objeto.Codigo + ');"><img src="' + ROOT_URL + 'Content/img/deletar.png" height="20" width="20" class="cursor-pointer" title="Excluir" ></a>';
                    }
                }
            ]
        });
    },
    atualizar: function () {
        tabela.ajax.reload();
    },
    excluir: function (id) {
        $("#dialog-confirm").dialog({
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            buttons: {
                "Sim": function () {
                    $.ajax({
                        type: "GET",
                        url: ROOT_URL + "api/Pessoa/Delete",
                        data: { "id": id },
                        success: function (result) {
                            dialog.dialog("close");
                            grid.atualizar();

                        }
                    });
                    $(this).dialog("close");
                },
                "Não": function () {
                    $(this).dialog("close");
                }
            }
        });


    }
}

var modal = {
    criarModal: function () {
        dialog = $("#dialog-form").dialog({
            autoOpen: false,
            height: 400,
            width: 240,
            modal: true,
            buttons: {
                "Incluir": modal.incluir,
                Cancelar: function () {
                    dialog.dialog("close");

                }
            },
            close: function () {
                $("#txtNome").removeClass("ui-state-error");
            }
        });
    },
    eventos: function () {
        $("#btnIncluir").on("click", function () {
            modal.abrirModal("", "", "", 0);
        });
    },
    abrirModal: function (nome, endereco, cidade, codigo) {
        $("#txtNome").val(nome);
        $("#txtEndereco").val(endereco);
        $("#txtCidade").val(cidade);
        $("#hddCodigo").val(codigo);
        dialog.dialog("open");
    },
    incluir: function () {
        if (modal.validar($("#txtNome"))) {
            var data = $('form').serialize();
            $.ajax({
                type: "POST",
                url: ROOT_URL + "api/Pessoa/Salvar",
                data: data,
                success: function (result) {
                    dialog.dialog("close");
                    grid.atualizar();

                }
            });
        }
    },
    validar: function (campo) {
        if (campo.val() == "") {
            campo.addClass("ui-state-error");
            return false;
        } else {
            campo.removeClass("ui-state-error");
            return true;
        }
    }
}