module Utils {
    declare var $;
    declare var RegExp;
    export function createMask(input: HTMLInputElement, mask?: any) {        
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
                input.addEventListener("input", e => {
                    $(input).mask("000.000.000,00", { reverse: true });
                });
                break;

            case "cpf":
                input.addEventListener("input", e => {
                    $(input).mask("900.000.000-00", { reverse: true });
                });

            case "":
                console.log();

            default:
                if (mask) $(input).mask(mask);
        }
    }

    export function converteFormatoMoeda(valor: number) {
        var joined,
            temp = [],
            inteiros = new RegExp(/\d+(?=\.|$)/g).exec(valor),
            decimais = new RegExp(/[(?=\.)](\d+$)/g).exec(valor),
            tamanho = inteiros.toString().length,
            separadorLen = 3,
            resto = tamanho % separadorLen,
            i = 0,
            total = inteiros.length + resto + tamanho
            temp = inteiros.toString().split('');

        console.log(decimais);
            
        //if (!valor) throw 'Valor Nulo';
        //var inteiros = new RegExp(/\d+(?=\,)/).exec(valor).toString();
        if (tamanho > separadorLen) {
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
            joined = valor;
        }
        console.log(joined);
        if (decimais) {
            console.log(decimais[0].replace(".", ","));
            joined += decimais[0].replace(".", ",") ;
        } else {
            joined += ",00";
        }
                         
        return joined;            
    }
} 