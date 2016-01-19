module Utils {
	declare var $;
	declare var RegExp;

	//Cria mascara de um lista de Inputs, selecionados pela classe (document.getElementsByClassName("classe"))
	export function createMaskClass(input: Array<HTMLInputElement>, mask: any) {
		for (var element in input) {
			if (input.hasOwnProperty(element)) {
				createMask(input[element], mask);
			}
		}
	}

	export function createMask(input: HTMLInputElement, mask: any) {

		switch (mask) {

			case "telefone":
				maskEvent(input, null);
				input.addEventListener("input", e => maskEvent(input, e));
				break;

			case "moeda":
                if (input.value != null) {
                    if (input.value) {
                        input.value = Utils.moneyFormatConvert(input.value.replace(",", "."));
                    }
				}

				$(input).mask("000.000.000,00", {
					onChange: function (e) {

						var once = false;

						if (input.value.length === 0) {
							//input.value = "";
							once = true;
						}

						if ((input.value.length === 1 && once) && event.keyCode != 8) {
							input.value = "0,0" + input.value;
						}

						if ((input.value.length === 2 || input.value.length === 3) && event.keyCode != 8) {
							input.value = "0," + input.value;
						}

						if (input.value.length > 5) {
							input.value = input.value.replace(/^0+/, "");
						} else {
							input.value = input.value.replace(/^0+(?=\d)\.?/, "");
						}
					},
					reverse: true,
					watchInterval: 200
				});
				break;

			case "cpf":
				if (input.value == undefined) {
					maskEventCnpj(input, undefined, input.innerHTML);
					input.addEventListener("input", e => maskEventCnpj(input, e, input.innerHTML));
				}
				else {
					maskEventCnpj(input, undefined, input.value);
					input.addEventListener("input", e => maskEventCnpj(input, e, input.value));
				}
				break;

			case "numero":
				$(input).mask("#", { reverse: true });
				break;

			case "rg":
				$(input).mask("900.000.000-0", { reverse: true });
				break;

			case "data":
				console.log(input.value);
				$(input).mask("00/00/0000");
				break;

			default:
				if (mask) $(input).mask(mask);
		}
	}

	function maskEvent(input: HTMLInputElement, e: any) {
		if (input.value.replace(/\D/g, "").length === 9) {
			$(input).mask("00000-0000");
		} else {
			$(input).mask("0000-00009");
		}
	}

	function maskEventCnpj(input: HTMLInputElement, e: any, value: any) {
		if (value.replace(/\D/g, "").length <= 11) {
			$(input).mask("000.000.000-009");
		}
		else {
			$(input).mask("00.000.000/0000-00");

		}
	}

	export function moneyFormatConvert(value: string) {
		var joined,
			temp = [],
			inteiros = new RegExp(/\d+(?=\.|$)/g).exec(value),
			regDec = new RegExp(/[(?=\.)](\d+$)/g).exec(value),
			decimais = regDec === null ? "" : regDec[0],
			tamanhoInt = inteiros[0].length,
			separadorLen = 3,
			resto = tamanhoInt % separadorLen,
			i = 0,
			total = resto + tamanhoInt;
		temp = inteiros[0].split("");

		if (tamanhoInt > separadorLen) {
			while (i < total - separadorLen) {

				if (i === 0 && resto !== 0) {
					temp.splice(resto, 0, ".");
					i += resto + 1;
				} else {
					i = resto === 0 && i === 0 ? i + separadorLen : i;
					temp.splice(i, 0, ".");
					i++;
				}
				i += separadorLen;
			}
			joined = temp.join("");
		} else {
			joined = inteiros;

		}
		if (decimais !== "") {
			joined = decimais.length === 2 ? joined += decimais.replace(".", ",") + "0" : joined += decimais.replace(".", ",");
		} else {
			joined += ",00";
		}

		return joined;
	}

	export function verifyBrowserInputDate() {
		var inputTest = document.createElement("input");
		inputTest.setAttribute("type", "date");

		if (inputTest.type === "text" && inputTest.getAttribute("type") === "date") {
			var inputs = <any>(document.querySelectorAll("input"));

			for (var i = 0; i < inputs.length; i++) {
				if (inputs[i].getAttribute("type") === "date") {

					$(function () {
						$(inputs[i]).datepicker({
							dateFormat: "dd/mm/yy",
							dayNames: ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado", "Domingo"],
							dayNamesMin: ["D", "S", "T", "Q", "Q", "S", "S", "D"],
							dayNamesShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb", "Dom"],
							monthNames: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
							monthNamesShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"]
						});
						var date = inputs[i].value.split("-");
						inputs[i].value = ("value", date[2] + date[1] + date[0]);
						createMask(inputs[i], "data");

					});
				};
			}
		}
	}

	export function convertFormatDate(date: string) {
		var dash = new RegExp(/^[0-3][0-9]\/[0-1][0-9]\/[1-2][0-9]{3}$/).exec(date);
		//var slash = new RegExp("/^[1-2][0-9]{3}\-[0-1][0-9]\-[0-3][0-9]$/").exec(date);

		if (dash) {
			var convert = date.split("/");
			return convert[2] + "-" + convert[1] + "-" + convert[0];
		}

		return date;

	}

	export function initializeDataTables(table : string) {

		var columnTable = $(table +" > thead  th");
		var dateIndex = [];

		for (var i = 0; i < columnTable.length; i++) {
            if ((columnTable[i].innerHTML.indexOf("Data") > -1) || (columnTable[i].innerHTML.indexOf("Vencimento") > -1)) {
				dateIndex.push(i);
			}
		}
		$(table).addClass("responsive no-wrap");

		$(table).DataTable({
				language: {
				sEmptyTable: "Nenhum registro encontrado",
				sInfo: "Mostrando de _START_ até _END_ de _TOTAL_ registros",
				sInfoEmpty: "Mostrando 0 até 0 de 0 registros",
				sInfoFiltered: "(Filtrados de _MAX_ registros)",
				sInfoPostFix: "",
				sInfoThousands: ".",
				sLengthMenu: "_MENU_ resultados por página",
				sLoadingRecords: "Carregando...",
				sProcessing: "Processando...",
				sZeroRecords: "Nenhum registro encontrado",
				sSearch: "Pesquisar",
				oPaginate: {
					sNext: "Próximo",
					sPrevious: "Anterior",
					sFirst: "Primeiro",
					sLast: "Último"
				},
				oAria: {
					sSortAscending: ": Ordenar colunas de forma ascendente",
					sSortDescending: ": Ordenar colunas de forma descendente"
				},
				decimal: ","
			},

			columnDefs: [
				{ type: "date-eu", targets: dateIndex }

			],

			colReorder: true,
			responsive: true
		});
	}

	export function manterTela(form){
		form.append('<input type="hidden" id="novo" name="novo" value="1"/>');
    }

    export function primeiroDiaMes(d: string, formato: string): string {
        var itensData;
        var data;

        if (d.indexOf('/') >= 0) {
            itensData = d.split('/');
        } else if(d.indexOf('-') >= 0 ) {
            itensData = d.split('-');
        }

        switch (formato) {
        case 'YMD':
            data = new Date(itensData[0], itensData[1] - 1, 1);

            break;

        case 'DMY':
            data = new Date(itensData[2], itensData[1] - 1, 1);

            break;

        case 'MDY':
            data = new Date(itensData[2], itensData[0] - 1, 1);

            break;
                    
        default:
        }


        return data.toISOString().substring(0, 10);

    }

    export function ultimoDiaMes(d: string, formato: string): string {
        var itensData;
        var data;

        if (d.indexOf('/') >= 0) {
            itensData = d.split('/');
        } else if (d.indexOf('-') >= 0) {
            itensData = d.split('-');
        }

        switch (formato) {
            case 'YMD':
                data = new Date(itensData[0], itensData[1], 0);

                break;

            case 'DMY':
                data = new Date(itensData[2], itensData[1], 0);

                break;

            case 'MDY':
                data = new Date(itensData[2], itensData[0], 0);

                break;

            default:
        }


        return data.toISOString().substring(0, 10);
    }

    export function getValue(element: any):string {
        return element.val() != ''? element.val() : element.html()
    }
}