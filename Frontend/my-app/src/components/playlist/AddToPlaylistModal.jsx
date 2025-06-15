import React, { useEffect, useState } from 'react';
import axios from 'axios';

function AddToPlaylistModal({ songId, onClose }) {
  const [playlists, setPlaylists] = useState([]);
  const [selectedPlaylistId, setSelectedPlaylistId] = useState(null);
  const [newPlaylistName, setNewPlaylistName] = useState("");

  const BASE_URL = import.meta.env.VITE_API_BASE_URL || "http://localhost:5000";

  useEffect(() => {
    const fetchPlaylists = async () => {
      const token = localStorage.getItem("token");

      try {
        const res = await axios.get(`${BASE_URL}/api/playlist/mine`, {
          headers: {
            Authorization: `Bearer ${token}`
          }
        });
        console.log("Playlists:", res.data);
        setPlaylists(res.data);
      } catch (error) {
        console.error("Failed to fetch playlists:", error.response?.data || error.message);
      }
    };

    fetchPlaylists();
  }, [newPlaylistName]);

  const handleAddSongToPlaylist = async () => {
    if (!selectedPlaylistId) return;

    const token = localStorage.getItem("token");

    try {
      await axios.post(`${BASE_URL}/api/playlist/musics`, {
        playlistId: selectedPlaylistId,
        musicId: songId
      }, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });

      onClose();
    } catch (err) {
      console.error("Failed to add song to playlist:", err.response?.data || err.message);
    }
  };

  const handleCreateNewPlaylist = async () => {
    if (!newPlaylistName.trim()) return;

    const token = localStorage.getItem("token");

    try {
      const res = await axios.post(
        `${BASE_URL}/api/playlist/${newPlaylistName}`,
        {},
        {
          headers: { Authorization: `Bearer ${token}` }
        }
      );
      setPlaylists([...playlists, res.data]);
      setNewPlaylistName("");
    } catch (error) {
      console.error("Failed to create playlist:", error.response?.data || error.message);
    }
  };

  return (
    <div style={{
      background: "#000000aa",
      position: "fixed",
      top: 0, left: 0, right: 0, bottom: 0,
      display: "flex", justifyContent: "center", alignItems: "center"
    }}>
      <div style={{
        background: "#1c1f26",
        padding: "24px",
        borderRadius: "12px",
        width: "400px",
        color: "white"
      }}>
        <h3 style={{ marginBottom: "16px" }}>Add to Playlist</h3>

        {playlists.length === 0 ? (
          <p style={{ color: '#ccc', marginBottom: '12px' }}>You don't have any playlists yet.</p>
        ) : (
          playlists.map((p) => (
            <div key={p.playlistId} style={{ marginBottom: '8px', color: 'white' }}>
              <label style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
                <input
                  type="radio"
                  name="playlist"
                  value={p.playlistId}
                  checked={selectedPlaylistId === p.playlistId}
                  onChange={() => setSelectedPlaylistId(p.playlistId)}
                />
                <span>{p.playlistName}</span>
              </label>
            </div>
          ))
        )}

        <input
          type="text"
          placeholder="New playlist name"
          value={newPlaylistName}
          onChange={(e) => setNewPlaylistName(e.target.value)}
          style={{ marginTop: '16px', padding: '8px', width: '100%', borderRadius: '6px', border: 'none' }}
        />
        <button onClick={handleCreateNewPlaylist} style={{
          marginTop: '8px',
          padding: '8px 12px',
          backgroundColor: '#00c896',
          border: 'none',
          borderRadius: '6px',
          cursor: 'pointer'
        }}>
          Create Playlist
        </button>

        <div style={{ marginTop: '16px', display: 'flex', justifyContent: 'flex-end', gap: '10px' }}>
          <button
            onClick={onClose}
            style={{
              background: '#444',
              padding: '6px 12px',
              borderRadius: '6px',
              border: 'none',
              color: 'white'
            }}
          >
            Close
          </button>
          <button
            onClick={handleAddSongToPlaylist}
            disabled={!selectedPlaylistId}
            style={{
              backgroundColor: selectedPlaylistId ? '#00c896' : '#555',
              padding: '6px 12px',
              borderRadius: '6px',
              border: 'none',
              color: 'black',
              cursor: selectedPlaylistId ? 'pointer' : 'not-allowed'
            }}
          >
            Add
          </button>
        </div>
      </div>
    </div>
  );
}

export default AddToPlaylistModal;
