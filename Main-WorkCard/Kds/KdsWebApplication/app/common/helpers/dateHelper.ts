module modules.common{

    export class DateHelper {

        public static addDaysToDate = (date: Date, days:number)=> {
            date.setDate(date.getDate() + days);
        }

        public static formatDate = (date: Date): string=> {
            var month = date.getMonth() + 1;
            var days = date.getDate().toString();
            var days = GeneralFunctions.padLeft(days, 2, "0");
            var dateStr = days + "/" + month + "/" + date.getFullYear();
          
            return dateStr;
        }
        public static convertToDate = (str: string): Date => {
            var d = str.split('/');
            var dDate = new Date(+(d[2]), +(d[1]) - 1, +(d[0]));// new Date(sDate[2], sDate[1] - 1, sDate[0]);
           // date.setDate(date.getDate() + days);
            return dDate;
        }

       

       
    }
}