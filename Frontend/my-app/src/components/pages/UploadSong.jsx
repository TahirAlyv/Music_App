import React, { useState } from 'react';
import axios from 'axios';

function UploadSong() {
  const [title, setTitle] = useState('');
  const [artist, setArtist] = useState('');
  const [album, setAlbum] = useState('');
  const [genre, setGenre] = useState('');
  const [audio, setAudio] = useState(null);
  const [cover, setCover] = useState(null);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const token = localStorage.getItem("token");

    const formData = new FormData();
    formData.append('title', title);
    formData.append('artist', artist);
    formData.append('album', album);
    formData.append('genre', genre);
    formData.append('file', audio);
    formData.append('cover', cover);

    try {
      const response = await axios.post('http://localhost:5000/api/Music/upload', formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
          'Authorization': `Bearer ${token}`
        }
      });

      console.log('Song uploaded:', response.data);
      alert('Song uploaded successfully!');
    } catch (error) {
      console.error('Upload error:', error);
      alert('An error occurred while uploading the song.');
    }
  };

  return (
    <div
      style={{
        backgroundColor: '#1a1e21',
        color: 'white',
        minHeight: '100vh',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center'
      }}
    >
      <form
        onSubmit={handleSubmit}
        style={{
          display: 'flex',
          flexDirection: 'column',
          gap: '12px',
          maxWidth: '400px',
          width: '100%',
          backgroundColor: '#2a2f33',
          padding: '30px',
          borderRadius: '12px',
          boxShadow: '0 0 10px rgba(0,0,0,0.3)'
        }}
      >
        <h2 style={{ textAlign: 'center' }}>Upload Song</h2>
        <input type="text" placeholder="Title" value={title} onChange={(e) => setTitle(e.target.value)} required />
        <input type="text" placeholder="Artist" value={artist} onChange={(e) => setArtist(e.target.value)} />
        <input type="text" placeholder="Album" value={album} onChange={(e) => setAlbum(e.target.value)} />
        <input type="text" placeholder="Genre" value={genre} onChange={(e) => setGenre(e.target.value)} />
        <label>Cover Image</label>
        <input type="file" accept="image/*" onChange={(e) => setCover(e.target.files[0])} />
        <label>Audio File</label>
        <input type="file" accept="audio/*" onChange={(e) => setAudio(e.target.files[0])} required />
        <button type="submit" style={{
          padding: '10px',
          backgroundColor: '#28a745',
          color: 'white',
          border: 'none',
          borderRadius: '6px'
        }}>
          Upload
        </button>
      </form>
    </div>
  );
}

export default UploadSong;
