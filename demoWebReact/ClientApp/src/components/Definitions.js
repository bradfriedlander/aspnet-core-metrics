﻿import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/Definitions';

class Definitions extends Component {
    constructor() {

    }

    componentWillMount() {
        // This method runs when the component is first added to the page
        const startDateIndex = parseInt(this.props.match.params.startDateIndex, 10) || 0;
        this.props.requestDefinitions(startDateIndex);
    }

    componentWillReceiveProps(nextProps) {
        // This method runs when incoming props (e.g., route params) change
        const startDateIndex = parseInt(nextProps.match.params.startDateIndex, 10) || 0;
        this.props.requestDefinitions(startDateIndex);
    }

    render() {
        return (
            <div>
                <h1>Defintions</h1>
                <p>This component demonstrates managing an local interface to demoWebApi</p>
                {renderDefinitions(this.props)}
                {renderPagination(this.props)}
            </div>
        );
    }

    edit = (id) => {
        alert("Updating: {id}");
    }
}

function renderDefinitions(props) {
    return (
        <div>
            <h3>Packet ID: {props.definitionPacket.packetId}</h3>
            <table className='table'>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Disabled?</th>
                        <th>Name</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {props.definitionPacket.definitions.map(definition =>
                        <tr key={definition.definitionId}>
                            <td>{definition.definitionId}</td>
                            <td><input type="checkbox" checked={definition.isDeleted} /></td>
                            <td>{definition.name}</td>
                            <td><Link className='btn btn-default' onclick={() => this.edit(definition.definitionId)}>Update</Link></td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
}

function renderPagination(props) {
    const prevStartDateIndex = (props.startDateIndex || 0) - 5;
    const nextStartDateIndex = (props.startDateIndex || 0) + 5;

    return <p className='clearfix text-center'>
        <Link className='btn btn-default pull-left' to={`/definitions/${prevStartDateIndex}`}>Previous</Link>
        <Link className='btn btn-default pull-right' to={`/definitions/${nextStartDateIndex}`}>Next</Link>
        {props.isLoading ? <span>Loading...</span> : []}
    </p>;
}

export default connect(
    state => state.definitions,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Definitions);
