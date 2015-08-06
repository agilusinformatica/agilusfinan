module Utils {
    declare var $;
    declare var RegExp;

    export function createMask(input: HTMLInputElement, mask: any) {          
           
        switch (mask) {

            case "telefone":
                input.addEventListener("input", e => {
                    if (input.value.replace(/\D/g, "").length === 9) {
                        $(input).mask("00000-0000");
                    } else {
                        $(input).mask("0000-00009");
                    }
                });
                break;

            case "moeda":                
                $(input).mask("000.000.000,00", {
                    onChange: function (e) {

                        var once = true;      

                        if (input.value.length === 0) {
                            once = true;
                        }

                        if ((input.value.length === 1 && once) && event.keyCode != 8) {
                            console.log(e.keyCode);
                            input.value = '0,0' + input.value;
                            once = false;
                        }

                        if ((input.value.length === 2 || input.value.length === 3) && event.keyCode != 8) {
                            input.value = '0,' + input.value;                            
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
                $(input).mask("900.000.000-00", { reverse: true });
                break;

            case "numero":
                $(input).mask("#", {reverse:true});
                break;

            case "":
                console.log("Vazio");
                break;

            default:
                if (mask) $(input).mask(mask);
        }
    }

    export function moneyFormatConvert(value: number) {
        var joined,
            temp = [],
            inteiros = new RegExp(/\d+(?=\.|$)/g).exec(value)[0],
            regDec = new RegExp(/[(?=\.)](\d+$)/g).exec(value),
            decimais = regDec === null? '': regDec[0],
            tamanhoInt = inteiros.length,
            separadorLen = 3,
            resto = tamanhoInt % separadorLen,
            i = 0,
            total = resto + tamanhoInt
            temp = inteiros.split('');

        if (tamanhoInt > separadorLen) {
            while (i < total - separadorLen) {

                if (i === 0 && resto !== 0) {
                    temp.splice(resto, 0, '.');
                    i += resto + 1;
                } else {
                    i = resto === 0 && i === 0 ? i + separadorLen : i;
                    temp.splice(i, 0, '.');
                    i++;
                }
                i += separadorLen;
            }

            joined = temp.join('')

        } else {
            joined = inteiros;
        }
        if (decimais) {
            joined = decimais.length === 2 ? joined += decimais.replace(".", ",") + "0" : joined += decimais.replace(".", ",");
        } else {
            joined += ",00";
        }                         
        return joined;            
    }
} 