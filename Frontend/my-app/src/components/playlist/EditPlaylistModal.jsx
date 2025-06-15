import React, { useState } from 'react';
import axios from 'axios';

const EditPlaylistModal = ({ playlist, onClose, onUpdated }) => {
  const [newName, setNewName] = useState(playlist.playlistName);
  const BASE_URL = import.meta.env.VITE_API_BASE_URL || "http://localhost:5000";

  const handleUpdate = async () => {
    const token = localStorage.getItem("token");
    try {
      const res = await axios.post(`${BASE_URL}/api/playlist/rename`, {
        playlistId: playlist.playlistId,
        playlistName: newName
      }, {
        headers: { Authorization: `Bearer ${token}` }
      });

      onUpdated(newName); 
      onClose();          
    } catch (err) {
      alert("Update failed");
    }
  };

  return (
    <div style={{
      position: 'fixed', top: 0, left: 0, right: 0, bottom: 0,
      backgroundColor: '#000000aa', display: 'flex', justifyContent: 'center', alignItems: 'center', zIndex: 9999
    }}>
      <div style={{ background: '#1c1f26', padding: '24px', borderRadius: '12px', color: 'white' }}>
        <h3>Rename Playlist</h3>
        <input
          value={newName}
          onChange={(e) => setNewName(e.target.value)}
          placeholder="New name"
          style={{ padding: '8px', width: '100%', marginBottom: '12px', borderRadius: '6px' }}
        />
        <div style={{ display: 'flex', justifyContent: 'flex-end', gap: '10px' }}>
          <button onClick={onClose} style={{ background: '#666', padding: '6px 12px', borderRadius: '6px', color: 'white' }}>Cancel</button>
          <button onClick={handleUpdate} style={{ background: '#00c896', padding: '6px 12px', borderRadius: '6px', color: '#1c1f26' }}>Update</button>
        </div>
      </div>
    </div>
  );
};

export default EditPlaylistModal;
