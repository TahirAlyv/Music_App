import React from 'react';

import { Route, Routes } from 'react-router-dom';
import Login from './components/pages/Login';
import Register from './components/pages/Register';
import Home from './components/pages/Home';
import UploadSong from './components/pages/UploadSong';
import MyList from './components/pages/MyList';
import PlaylistDetailPage from './components/playlist/PlaylistDetailPage';
import Favorites from './components/favorite/favorite';


function App() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path='/Home' element= {<Home/>}/>
      <Route path='/upload' element={<UploadSong/>}/>
      <Route path='mylist' element={<MyList/>}/>
      <Route path="/playlist/:id" element={<PlaylistDetailPage />} />
      <Route path='favorite' element={<Favorites/>}/>
    </Routes>
  );
}

export default App;
