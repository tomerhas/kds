module modules.workcard {

    export class GeneralEvents {
        public static OVED_PEILUT_CHANGED: string = "ovedPeiluyot-changed";
        public static OVED_DETAILS_CHANGED: string = "ovedDetails-changed";
        public static ADVANCE_DATE_BY_DAYS: string = "show.nextdate";
        public static SHOW_NEXT_ERROR_DAY: string = "show.nextErrordate";

        public static MKT_VISUT: number = 4;
        public static MKT_VISA: number = 6;
        public static MKT_SHERUT: number = 1;
        public static MKT_EMPTY: number = 2;
        public static MKT_NAMAK: number = 3;
        public static MKT_ELEMENT: number = 5;

    }

}