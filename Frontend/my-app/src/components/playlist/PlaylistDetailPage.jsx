import React, { useEffect, useState } from 'react';
import { useParams, useLocation } from 'react-router-dom';
import axios from 'axios';

const PlaylistDetailPage = () => {
  const { id } = useParams();
  const location = useLocation();
  const [playlistName, setPlaylistName] = useState(location.state?.name || "Playlist");
  const [songs, setSongs] = useState([]);
  const [selectedSong, setSelectedSong] = useState(null);
  const [audioUrl, setAudioUrl] = useState(null);
  const BASE_URL = import.meta.env.VITE_API_BASE_URL || "http://localhost:5000";

  useEffect(() => {
    const fetchMusics = async () => {
      const token = localStorage.getItem("token");

      try {
        const res = await axios.get(`${BASE_URL}/api/playlist/${id}/musics`, {
          headers: { Authorization: `Bearer ${token}` }
        });

        setSongs(res.data);
        console.log(res.data);
      } catch (err) {
        console.error("Failed to fetch songs:", err.response?.data || err.message);
      }
    };

    fetchMusics();
  }, [id]);

  const handleSongClick = (song) => {
    setSelectedSong(song);
    setAudioUrl(`${BASE_URL}${song.filePath}`);
  };

  const handleDelete = async (songId) => {
    const token = localStorage.getItem("token");

    try {
      await axios.delete(`${BASE_URL}/api/playlist/${id}/musics/${songId}`, {
        headers: { Authorization: `Bearer ${token}` }
      });
      setSongs(prev => prev.filter(song => song.id !== songId));
      if (selectedSong?.id === songId) {
        setSelectedSong(null);
        setAudioUrl(null);
      }
    } catch (err) {
      console.error("Delete failed:", err.response?.data || err.message);
    }
  };

  return (
    <div style={{
      display: 'flex',
      backgroundColor: '#0d0f13',
      color: 'white',
      height: '100vh'
    }}>
      {/* Left - Song List */}
      <div style={{
        flex: 2,
        padding: '40px',
        overflowY: 'auto',
        borderRight: '1px solid #1f1f1f'
      }}>
        <h2 style={{ marginBottom: '24px', color: '#00c896', fontSize: '24px' }}>
          Playlist: {playlistName}
        </h2>

        {songs.length === 0 ? (
          <p style={{ color: '#aaa' }}>No songs in this playlist yet.</p>
        ) : (
          <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
            {songs.map((song, index) => (
              <div
                key={song.id}
                style={{
                  display: 'flex',
                  justifyContent: 'space-between',
                  alignItems: 'center',
                  padding: '16px',
                  backgroundColor: selectedSong?.id === song.id ? '#00332e' : '#1a1e24',
                  borderRadius: '12px',
                  boxShadow: '0 2px 8px rgba(0,0,0,0.2)',
                  transition: '0.2s ease',
                  gap: '16px'
                }}
              >
                {/* Song Info */}
                <div
                  onClick={() => handleSongClick(song)}
                  style={{ cursor: 'pointer', flexGrow: 1 }}
                >
                  <div style={{ fontSize: '16px', fontWeight: 'bold', color: '#fff' }}>
                    {index + 1}. {song.title}
                  </div>
                  <div style={{ fontSize: '13px', color: '#ccc' }}>
                    {song.artist} • {song.album}
                  </div>
                </div>

                {/* Delete Button */}
                <button
                  onClick={() => handleDelete(song.id)}
                  style={{
                    backgroundColor: '#ff4d4f',
                    border: 'none',
                    color: 'white',
                    padding: '6px 12px',
                    borderRadius: '6px',
                    cursor: 'pointer',
                    fontWeight: 'bold',
                    fontSize: '13px',
                    flexShrink: 0
                  }}
                >
                  Delete
                </button>
              </div>
            ))}
          </div>
        )}
      </div>

      {/* Right - Selected Song */}
      <div style={{
        flex: 1,
        backgroundColor: '#12161c',
        padding: '32px',
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center'
      }}>
        {selectedSong ? (
          <>
            <img
              src={`${BASE_URL}${selectedSong.coverImagePath}`}
              alt="Cover"
              style={{
                width: '80%',
                borderRadius: '16px',
                marginBottom: '24px',
                boxShadow: '0 4px 16px rgba(0,0,0,0.5)',
                objectFit: 'cover'
              }}
            />
            <h3 style={{ fontSize: '20px', marginBottom: '8px' }}>{selectedSong.title}</h3>
            <p style={{ color: '#ccc', marginBottom: '4px' }}>{selectedSong.artist}</p>
            <p style={{ fontSize: '13px', color: '#888' }}>
              {selectedSong.album} • {selectedSong.genre}
            </p>
            {selectedSong.duration && (
              <p style={{ fontSize: '12px', color: '#666', marginTop: '4px' }}>
                Duration: {selectedSong.duration}
              </p>
            )}
            <audio controls style={{
              marginTop: '20px',
              width: '100%',
              backgroundColor: 'transparent'
            }}>
              <source src={audioUrl} type="audio/mpeg" />
              Your browser does not support the audio element.
            </audio>
          </>
        ) : (
          <p style={{ color: '#777' }}>Select a song</p>
        )}
      </div>
    </div>
  );
};

export default PlaylistDetailPage;
