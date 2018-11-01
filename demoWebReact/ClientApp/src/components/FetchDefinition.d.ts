import * as React from 'react';
import { RouteComponentProps } from 'react-router';
interface FetchDefinitionDataState {
    definitionList: DefinitionData[];
    loading: boolean;
}
export declare class FetchDefinition extends React.Component<RouteComponentProps<{}>, FetchDefinitionDataState> {
    constructor(props: any);
    render(): JSX.Element;
    private getAll;
    private handleDelete;
    private handleEdit;
    private handleUndelete;
    private renderEmployeeTable;
    private renderDeleteLink;
}
export declare class DefinitionData {
    definitionId: number;
    name: string;
    isDeleted: boolean;
}
export {};
