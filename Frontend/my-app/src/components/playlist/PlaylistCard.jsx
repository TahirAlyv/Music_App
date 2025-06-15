const PlaylistCard = ({ playlist, onDelete, onEdit, onClick }) => {
  return (
    <div
      onClick={() => onClick(playlist)}
      style={{
        background: '#1c1f26',
        padding: '20px',
        borderRadius: '12px',
        width: '220px',
        height: '150px',
        boxShadow: '0 4px 8px rgba(0,0,0,0.3)',
        cursor: 'pointer',
        position: 'relative'
      }}
    >
      <h4>{playlist.playlistName}</h4>
      <p style={{ color: '#aaa' }}>
        Number of songs: {playlist.songs?.length || 0}
      </p>

      <div style={{ position: 'absolute', top: '10px', right: '10px', display: 'flex', gap: '8px' }}>
        <button
          onClick={(e) => { e.stopPropagation(); onEdit(playlist); }}
          style={{
            background: '#00c896',
            color: '#1c1f26',
            border: 'none',
            borderRadius: '4px',
            padding: '4px 8px',
            cursor: 'pointer'
          }}
        >Edit</button>

        <button
          onClick={(e) => {
            e.stopPropagation();
            if (confirm("Are you sure you want to delete this playlist?")) {
              onDelete(playlist.playlistId);
            }
          }}
          style={{
            background: '#ff4d4f',
            color: 'white',
            border: 'none',
            borderRadius: '4px',
            padding: '4px 8px',
            cursor: 'pointer'
          }}
        >Delete</button>
      </div>
    </div>
  );
};

export default PlaylistCard;
