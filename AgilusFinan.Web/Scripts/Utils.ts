
module Utils {
    declare var $;
    export function createMask(input: HTMLInputElement, mask?: any) {        
        switch (mask) {

            case "telefone":
                input.addEventListener("input", event => {
                    if (input.value.replace(/\D/g, "").length === 9) {
                        $(input).mask("00000-0000");
                    } else {
                        $(input).mask("0000-00009");
                    }
                });
                break;

            case "moeda":

                break;

            default:
                if (mask) $(input).mask(mask);
        }
    }
} 