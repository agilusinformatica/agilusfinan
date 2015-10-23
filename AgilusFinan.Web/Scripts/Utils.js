var Utils;
(function (Utils) {
    //Cria mascara de um lista de Inputs, selecionados pela classe (document.getElementsByClassName("classe"))
    function createMaskClass(input, mask) {
        for (var element in input) {
            if (input.hasOwnProperty(element)) {
                createMask(input[element], mask);
            }
        }
    }
    Utils.createMaskClass = createMaskClass;

    function createMask(input, mask) {
        switch (mask) {
            case "telefone":
                maskEvent(input, null);
                input.addEventListener("input", function (e) {
                    return maskEvent(input, e);
                });
                break;

            case "moeda":
                if (input.value) {
                    input.value = Utils.moneyFormatConvert(input.value.replace(",", "."));
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
                    input.addEventListener("input", function (e) {
                        return maskEventCnpj(input, e, input.innerHTML);
                    });
                } else {
                    maskEventCnpj(input, undefined, input.value);
                    input.addEventListener("input", function (e) {
                        return maskEventCnpj(input, e, input.value);
                    });
                }
                break;

            case "numero":
                $(input).mask("#", { reverse: true });
                break;

            case "rg":
                $(input).mask("900.000.000-0", { reverse: true });
                break;

            default:
                if (mask)
                    $(input).mask(mask);
        }
    }
    Utils.createMask = createMask;

    function maskEvent(input, e) {
        if (input.value.replace(/\D/g, "").length === 9) {
            $(input).mask("00000-0000");
        } else {
            $(input).mask("0000-00009");
        }
    }

    function maskEventCnpj(input, e, value) {
        if (value.replace(/\D/g, "").length <= 11) {
            $(input).mask("000.000.000-009");
        } else {
            $(input).mask("00.000.000/0000-00");
        }
    }

    function moneyFormatConvert(value) {
        var joined, temp = [], inteiros = new RegExp(/\d+(?=\.|$)/g).exec(value), regDec = new RegExp(/[(?=\.)](\d+$)/g).exec(value), decimais = regDec === null ? "" : regDec[0], tamanhoInt = inteiros[0].length, separadorLen = 3, resto = tamanhoInt % separadorLen, i = 0, total = resto + tamanhoInt;
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
    Utils.moneyFormatConvert = moneyFormatConvert;
})(Utils || (Utils = {}));
//# sourceMappingURL=Utils.js.map
