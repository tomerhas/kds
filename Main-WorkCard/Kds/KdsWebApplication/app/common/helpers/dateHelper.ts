module modules.common{

    export class DateHelper {
        public static addDaysToDate = (date: Date, days:number)=> {
            date.setDate(date.getDate() + days);
        }

        public static formatDate = (date: Date): string=> {
            var month = date.getMonth() + 1;
            var dateStr = date.getDate() + "/" + month + "/" + date.getFullYear();
            return dateStr;
        }

       

       
    }
}