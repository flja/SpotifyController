import * as React from 'react';

import '../Template.css';
import './Style.css';


export default class Track extends React.Component {

    constructor(props) {
        super(props);
        this.state = { value: "", loading: true };
    }

    render() {
        return (
            <React.Fragment>
                <div className="search">
                    <form onSubmit={this.handleSubmit}>
                        <label>
                            Name:
                            <input type="text" value={this.state.value} onChange={this.updateInput} />
                        </label>
                        <input type="submit" value="Submit" />
                    </form>
                </div>
            </React.Fragment>
        );
    }

    //async updateInput() {
    //    event
    //}

    async QueueTrack(trackId) {
        const response = await fetch(`API/SpotifyAPI/QueueTrack?trackId=${this.props.id}`,{
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({"trackId": trackId}),
        });

        if (response.status == 200) {
            document.getElementById(`${this.props.id}`).querySelector("Button").innerText = "Queued!"
                document.getElementById(`${this.props.id}`).querySelector("Button").style.background = "green";
            setTimeout(() => {
                document.getElementById(`${this.props.id}`).querySelector("Button").innerText = "Queue";
                document.getElementById(`${this.props.id}`).querySelector("Button").style.background = "white";
            }, 1000);
        } else if (response.status == 401) {
            window.location.replace("/API/SpotifyAPI/Authorize");
        } else {
            console.error(`Error ${response.status} - Queue failed!`)
            document.getElementById(`${this.props.id}`).querySelector("Button").innerText = "Failed!"
            document.getElementById(`${this.props.id}`).querySelector("Button").style.background = "#C11B17";
            setTimeout(() => {
                document.getElementById(`${this.props.id}`).querySelector("Button").innerText = "Queue";
                document.getElementById(`${this.props.id}`).querySelector("Button").style.background = "white";
            }, 1000);
        }
    }

    RenderIfWidthIsOver(minWidth, component) {
        return (window.innerWidth > minWidth && component)
    }
}