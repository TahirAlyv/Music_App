// src/pages/MyList.jsx
import React, { useEffect, useState } from 'react';
import axios from 'axios';
import PlaylistCard from '../../components/playlist/PlaylistCard';
import EditPlaylistModal from '../../components/playlist/EditPlaylistModal';
import { useNavigate } from 'react-router-dom';

const MyList = () => {
  const [playlists, setPlaylists] = useState([]);
  const [showEditModal, setShowEditModal] = useState(false);
  const [selectedPlaylist, setSelectedPlaylist] = useState(null);
  const BASE_URL = import.meta.env.VITE_API_BASE_URL || "http://localhost:5000";
  const navigate = useNavigate();

  useEffect(() => {
    const fetchPlaylists = async () => {
      const token = localStorage.getItem("token");

      try {
        const res = await axios.get(`${BASE_URL}/api/playlist/mine`, {
          headers: { Authorization: `Bearer ${token}` }
        });
        setPlaylists(res.data);
      } catch (error) {
        console.error("Failed to fetch playlists:", error.response?.data || error.message);
      }
    };

    fetchPlaylists();
  }, []);

  const handleDelete = async (playlistId) => {
    const token = localStorage.getItem("token");
    const confirmDelete = window.confirm("Are you sure you want to delete this playlist?");
    if (!confirmDelete) return;

    try {
      await axios.delete(`${BASE_URL}/api/playlist/${playlistId}`, {
        headers: { Authorization: `Bearer ${token}` }
      });
      setPlaylists(prev => prev.filter(p => p.playlistId !== playlistId));
    } catch (err) {
      console.error("Delete error:", err.response?.data || err.message);
      alert("Delete failed");
    }
  };

  const handleEdit = (playlist) => {
    setSelectedPlaylist(playlist);
    setShowEditModal(true);
  };

  return (
    <div style={{ padding: "40px", color: "white" }}>
      <h2>My Playlists</h2>
      <div style={{ display: "flex", flexWrap: "wrap", gap: "20px", marginTop: "20px" }}>
        {playlists.map(p => (
          <PlaylistCard
            key={p.playlistId}
            playlist={p}
            onClick={() => navigate(`/playlist/${p.playlistId}`, {
              state: { name: p.playlistName }
            })}
            onDelete={handleDelete}
            onEdit={handleEdit}
          />
        ))}
      </div>

      {showEditModal && selectedPlaylist && (
        <EditPlaylistModal
          playlist={selectedPlaylist}
          onClose={() => setShowEditModal(false)}
          onUpdated={(newName) => {
            setPlaylists(prev =>
              prev.map(p =>
                p.playlistId === selectedPlaylist.playlistId
                  ? { ...p, playlistName: newName }
                  : p
              )
            );
            setShowEditModal(false);
          }}
        />
      )}
    </div>
  );
};

export default MyList;
