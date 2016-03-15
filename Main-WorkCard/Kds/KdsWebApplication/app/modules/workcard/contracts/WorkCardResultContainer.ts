module modules.workcard {

    export class WorkCardResultContainer {
        Sidurim: SidurimWC;
        FirstHityazvut: Hityatzvut;
        SecondHityazvut: Hityatzvut;
        DayDetails: {
            ZmanNesiot: {
                IsEnabled: boolean;
                Value: number;
                Attributes: AttributeField[];
            };
            Tachograf: {
                IsEnabled: boolean;
                Value: number;
                Attributes: AttributeField[];
            };
            Halbasha: {
                IsEnabled: boolean;
                Value: number;
                Attributes: AttributeField[];
            };
            Lina: {
                IsEnabled: boolean;
                Value: number;
                Attributes: AttributeField[];
            };
            HashlamaForDay: {
                IsEnabled: boolean;
                Value: number;
                Attributes: AttributeField[];
            };
            SibatHashlamaLeyom: {
                IsEnabled: boolean;
                Value: number;
                Attributes: AttributeField[];
            };
        };
        CardStatus: {
            Kodstatus: number;
            TeurStatus: string;
            ClassStr: string;
        };
        BRashemet: boolean;
        BMenahelBankShaot: boolean;
        OParams: clParametersDM;
    }

}