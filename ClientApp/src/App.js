import * as React from 'react';
import { Route } from 'react-router';

import { Home } from './components/Home';

import Layout from './components/Layout';

import './custom.css'
import Content from './components/Content';

import Track from './components/Spotify/Track';
import Playlist from './components/Spotify/Playlist';
import PlaylistLink from './components/Spotify/PlaylistLink';
import PlaylistOverview from './components/Spotify/PlaylistOverview';
import Search from './components/Spotify/Search';

export default () => ( 
    <Layout Title="Spotify">

        {RenderIfWidthIsOver(
            //    800,
        )}

        <Content>
            <Route exact path='/' component={Home} />
            <Route exact path='/playlists' component={PlaylistOverview} />
            <Route exact path='/search' component={Search} />
            <Route path='/playlist/:id' component={Playlist} />
            <Route path='/playlistlink/:id' component={PlaylistLink} />
        </Content>

        {RenderIfWidthIsOver(
            //    800,
        )}

    </Layout>

);
  



function RenderIfWidthIsOver(minWidth, component) {
    return (window.innerWidth > minWidth && component)
}
