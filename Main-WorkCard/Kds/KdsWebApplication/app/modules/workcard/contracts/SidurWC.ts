module modules.workcard {

    export class SidurWC {
        CollapseSrc: {
            IsEnabled: boolean;
            Value: string;
            Attributes: AttributeField[];
        };
        MisparSidur: {
            IsEnabled: boolean;
            Value: number;
            Attributes: AttributeField[];
        };
        FullShatHatchala: {
            IsEnabled: boolean;
            Value: Date;
            Attributes: AttributeField[];
        };
        FullShatGmar: {
            IsEnabled: boolean;
            Value: Date;
            Attributes: AttributeField[];
        };
        SibatHachtamaIn: {
            IsEnabled: boolean;
            Value: number;
            Attributes: AttributeField[];
        };
        SibatHachtamaOut: {
            IsEnabled: boolean;
            Value: number;
            Attributes: AttributeField[];
        };
        FullShatHatchalaLetashlum: {
            IsEnabled: boolean;
            Value: Date;
            Attributes: AttributeField[];
        };
        FullShatGmarLetashlum: {
            IsEnabled: boolean;
            Value: Date;
            Attributes: AttributeField[];
        };
        Hariga: {
            Ivalue: number;
            Attributes: AttributeField[];
        };
        Pizul: {
            IsEnabled: boolean;
            Value: number;
            Attributes: AttributeField[];
        };
        Hashlama: {
            IsEnabled: boolean;
            Value: string;
            Attributes: AttributeField[];
        };
        Hamara: {
            IsEnabled: boolean;
            Value: string;
            Attributes: AttributeField[];
        };
        OutMichsa: {
            IsEnabled: boolean;
            Value: string;
            Attributes: AttributeField[];
        };
        LoLetashlum: {
            IsEnabled: boolean;
            Value: number;
            Attributes: AttributeField[];
        };
        SidurActive: {
            IsEnabled: boolean;
            Value: boolean;
            Attributes: AttributeField[];
        };
        PeilutList: PeilutWC[];
    }
}
