
$(document).ready(function () {
    modal.criarModal();
    modal.eventos();
    grid.carregar();
    modal.preencherCombo();
});

var tabela;
var grid = {
    carregar: function () {
        var filterValues = {};
        tabela = $('#tblJogo').DataTable({
            ajax: function (data, callback, settings) {
                filterValues.draw = data.draw;
                filterValues.start = data.start;
                filterValues.length = data.length;
                $.ajax({
                    url: ROOT_URL + "api/Jogo/Consultar",
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
                { data: "Titulo" },
                { data: "Genero" },
                { data: "NomeAmigo" },
                {
                    mData: null, orderable: false, title: 'Ação', sWidth: '7%', mRender: function (objeto) {
                        var retorno = '<a href="javascript:void(0);" class="js-alterar"  onclick="modal.abrirModal(\'' + objeto.Titulo + '\',\'' + objeto.Genero + '\',\'' + objeto.Codigo + '\');" >'
                            + '<img src="' + ROOT_URL + 'Content/img/editar.png" height="20" width="20" alt="Alterar" title="Alterar" class="cursor-pointer"></a> &nbsp;'
                            + '<a href="javascript:void(0);" class="js-excluir" onclick="grid.excluir(' + objeto.Codigo + ');"><img src="' + ROOT_URL + 'Content/img/deletar.png" height="20" width="20" class="cursor-pointer" title="Excluir" ></a>'
                         + ' &nbsp;';
                        if (objeto.NomeAmigo == "") {
                            retorno += '<a href="javascript:void(0);" class="js-emprestar" onclick="modal.abrirModalEmprestar(' + objeto.Codigo + ');"><img src="' + ROOT_URL + 'Content/img/emprestar.png" height="20" width="20" class="cursor-pointer" title="Emprestar" ></a>'
                            ;
                        } else {
                            retorno += '<a href="javascript:void(0);" class="js-devolver" onclick="grid.devolver(' + objeto.Codigo + ');"><img src="' + ROOT_URL + 'Content/img/devolver.png" height="20" width="20" class="cursor-pointer" title="Devolver" ></a>'
                            ;
                        }
                        return retorno;
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
                        url: ROOT_URL + "api/Jogo/Delete",
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


    },
    devolver: function (id) {
        $("#dialog-confirm-devolver").dialog({
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            buttons: {
                "Sim": function () {
                    $.ajax({
                        type: "GET",
                        url: ROOT_URL + "api/Jogo/Devolver",
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
        dialog2 = $("#dialog-Emprestar").dialog({
            autoOpen: false,
            height: 200,
            width: 260,
            modal: true,
            buttons: {
                "Emprestar": modal.emprestar,
                Cancelar: function () {
                    dialog2.dialog("close");

                }
            },
            close: function () {
                $("#ddlAmigo").removeClass("ui-state-error");
            }
        });
    },
    eventos: function () {
        $("#btnIncluir").on("click", function () {
            modal.abrirModal("", "", "", 0);
        });
    },
    abrirModal: function (titulo, genero, codigo) {
        $("#txtTitulo").val(titulo);
        $("#txtGenero").val(genero);
        $("#hddCodigo").val(codigo);
        dialog.dialog("open");
    },
    abrirModalEmprestar: function (codigo) {
        $("#hddCodigoEmprestar").val(codigo);
        dialog2.dialog("open");
    },
    incluir: function () {
        if (modal.validar($("#txtTitulo"))) {
            var data = $('#frmManter').serialize();
            $.ajax({
                type: "POST",
                url: ROOT_URL + "api/Jogo/Salvar",
                data: data,
                success: function (result) {
                    dialog.dialog("close");
                    grid.atualizar();

                }
            });
        }
    },
    emprestar: function () {
        if (modal.validar($("#ddlAmigo"))) {
            var data = $('#frmEmprestar').serialize();
            $.ajax({
                type: "POST",
                url: ROOT_URL + "api/Jogo/Emprestar",
                data: data,
                success: function (result) {
                    dialog2.dialog("close");
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
    },
    preencherCombo: function () {
        var ddlAmigo = $('#ddlAmigo');
        ddlAmigo.append($("<option></option>").val("").html("Selecione..."));
        $.ajax({
            type: "GET",
            url: ROOT_URL + "api/Pessoa/Consultar",
            data: '{name: "abc" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $.each(data.data, function () {
                    ddlAmigo.append($("<option></option>").val(this.Codigo).html(this.Nome));
                });
            },
            failure: function (response) {
                alert(response.d);
            }
        });
    },

}