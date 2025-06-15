import Navbar from "../navbar/Navbar";
import { useState, useEffect } from "react";
import axios from "axios";
import AddToPlaylistModal from "../../components/playlist/AddToPlaylistModal";

function Home() {
  const [songs, setSongs] = useState([]);
  const [favorites, setFavorites] = useState([]);
  const [selectedSong, setSelectedSong] = useState(null);
  const [audioUrl, setAudioUrl] = useState(null);
  const [showModal, setShowModal] = useState(false);

  const BASE_URL = "http://localhost:5000";
  const token = localStorage.getItem("token");
  const userId = token ? JSON.parse(atob(token.split(".")[1])).nameid : null;

  useEffect(() => {
    const fetchSongs = async () => {
      try {
        const response = await axios.get(`${BASE_URL}/api/music/musics`, {
          headers: { Authorization: `Bearer ${token}` },
        });
        setSongs(response.data);
      } catch (error) {
        console.error("Failed to fetch songs:", error);
      }
    };

    const fetchFavorites = async () => {
      try {
        const res = await axios.get(`${BASE_URL}/api/favorite`, {
          headers: { Authorization: `Bearer ${token}` },
        });
        setFavorites(res.data);
      } catch (error) {
        console.error("Favoriler alınamadı:", error);
      }
    };

    fetchSongs();
    fetchFavorites();
  }, [token, userId]);

  const handleSongClick = (song) => {
    setSelectedSong(song);
    setAudioUrl(`${BASE_URL}${song.filePath}`);
  };

const toggleFavorite = async (songId) => {
  try {
    if (favorites.includes(songId)) {
      await axios.delete(`${BASE_URL}/api/favorite/remove`, {
        params: { musicId: songId },
        headers: { Authorization: `Bearer ${token}` },
      });
      setFavorites((prev) => prev.filter((id) => id !== songId));
    } else {
      await axios.post(`${BASE_URL}/api/favorite/add`, null, {
        params: { musicId: songId },
        headers: { Authorization: `Bearer ${token}` },
      });
      setFavorites((prev) => [...prev, songId]);
    }
  } catch (error) {
    console.error("Favori güncelleme hatası:", error);
  }
};


  return (
    <div style={{ backgroundColor: "#0e1117", color: "white", height: "100vh", width: "100vw", overflow: "hidden" }}>
      <Navbar />

      <div style={{ display: 'flex', height: 'calc(100vh - 60px)' }}>
        <div style={{ flex: 2, padding: '24px', overflowY: 'auto' }}>
          <h2 style={{ marginBottom: '16px' }}>Song List</h2>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
            {songs.map((song, index) => (
              <div
                key={song.id}
                onClick={() => handleSongClick(song)}
                style={{
                  border: '1px solid #333',
                  borderRadius: '12px',
                  padding: '16px',
                  backgroundColor: '#1c1f26',
                  cursor: 'pointer',
                  display: 'flex',
                  alignItems: 'center',
                  gap: '16px'
                }}
              >
                <button style={{
                  background: 'white',
                  borderRadius: '50%',
                  width: '32px',
                  height: '32px',
                  display: 'flex',
                  justifyContent: 'center',
                  alignItems: 'center',
                  border: 'none',
                  fontWeight: 'bold',
                  color: '#1c1f26'
                }}>▶</button>
                <div>
                  <strong>{index + 1}. {song.title}</strong>
                  <div style={{ color: '#aaa', fontSize: '14px' }}>{song.artist} — {song.album}</div>
                </div>
                <button
                  onClick={(e) => { e.stopPropagation(); toggleFavorite(song.id); }}
                  style={{
                    background: "transparent",
                    border: "none",
                    fontSize: "20px",
                    color: favorites.includes(song.id) ? "red" : "#aaa",
                    cursor: "pointer",
                    marginLeft: "auto"
                  }}
                >
                  {favorites.includes(song.id) ? '❤️' : '🤍'}
                </button>
              </div>
            ))}
          </div>
        </div>

        <div style={{ flex: 1, backgroundColor: "#14171d", display: 'flex', flexDirection: 'column', justifyContent: 'center', alignItems: 'center', padding: '32px', borderLeft: '1px solid #222' }}>
          {selectedSong ? (
            <div style={{ width: '100%', maxWidth: '400px', textAlign: 'center', background: 'linear-gradient(145deg, #1c1f26, #2a2e37)', borderRadius: '24px', padding: '28px', boxShadow: '0 8px 24px rgba(0, 0, 0, 0.6)', transition: '0.3s all' }}>
              <div style={{ fontSize: '13px', color: '#888', marginBottom: '10px', textTransform: 'uppercase', letterSpacing: '1px' }}>Now Playing</div>
              <img src={`${BASE_URL}${selectedSong.coverImagePath}`} alt="Cover" style={{ width: '100%', borderRadius: '16px', marginBottom: '24px', boxShadow: '0 4px 12px rgba(0,0,0,0.3)', objectFit: 'cover' }} />
              <h2 style={{ marginBottom: '6px', fontSize: '20px', fontWeight: 'bold', color: '#f1f1f1' }}>{selectedSong.title}</h2>
              <p style={{ color: '#bbb', marginBottom: '6px' }}>{selectedSong.artist}</p>
              <p style={{ color: '#777', fontSize: '14px', fontStyle: 'italic' }}>{selectedSong.album} &nbsp;•&nbsp; {selectedSong.genre}</p>
              <audio controls style={{ marginTop: '24px', width: '100%', backgroundColor: '#0e1117', borderRadius: '20px', outline: 'none' }}>
                <source src={audioUrl} type="audio/mpeg" />
                Your browser does not support the audio element.
              </audio>
              <button style={{ marginTop: '24px', backgroundColor: '#00c896', color: '#1c1f26', border: 'none', padding: '12px 24px', borderRadius: '9999px', fontWeight: 'bold', cursor: 'pointer', transition: '0.3s ease', fontSize: '14px' }} onClick={() => setShowModal(true)}>
                + Add to Playlist
              </button>
            </div>
          ) : (
            <p style={{ color: '#777' }}>Select a song</p>
          )}
        </div>
      </div>

      {showModal && selectedSong && (
        <AddToPlaylistModal songId={selectedSong.id} onClose={() => setShowModal(false)} />
      )}
    </div>
  );
}

export default Home;
