import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { actionCreators } from '../store/Definitions';

class Definitions extends Component {
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
}

function renderDefinitions(props) {
    return (
        <table className='table'>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Disbaled?</th>
                    <th>Name</th>
                </tr>
            </thead>
            <tbody>
                {props.definitions.map(definition =>
                    <tr key={definition.Id}>
                        <td>{definition.Id}</td>
                        <td>{definition.IsDeleted}</td>
                        <td>{definition.Name}</td>
                    </tr>
                )}
            </tbody>
        </table>
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
