module modules.common {

    export class GeneralFunctions {
        public static padLeft = (str: string, length: number, c: string): string  => {
            var s = '', c = c || '0', len = (len || 2) - str.length;
            while (s.length < len) s += c;
            return s + str;
        }
    }
}


//////////////////////////**************   general functions ****************/////////////////////

function FocusText(obj) {
    obj.select();
}
function trim(str) {
    return str.replace(/^[\s]+/, '').replace(/[\s]+$/, '').replace(/[\s]{2,}/, ' ');
}
