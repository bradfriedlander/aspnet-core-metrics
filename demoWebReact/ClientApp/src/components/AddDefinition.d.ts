import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { DefinitionData } from './FetchDefinition';
interface AddDefinitionDataState {
    title: string;
    loading: boolean;
    definitionData: DefinitionData;
}
export declare class AddDefinition extends React.Component<RouteComponentProps<{}>, AddDefinitionDataState> {
    constructor(props: any);
    render(): JSX.Element;
    private handleSave;
    private handleCancel;
    private renderCreateForm;
}
export {};
