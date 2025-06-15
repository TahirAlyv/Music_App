import { NavLink } from 'react-router-dom';

function Navbar() {
  return (
    <nav
      style={{
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        padding: '16px',
        background: '#222',
        color: 'white'
      }}
    >
 
      <div style={{ display: 'flex', gap: '20px', alignItems: 'center' }}>
        <NavLink
          to="/mylist"
          style={({ isActive }) => ({
            color: isActive ? 'yellow' : 'white',
            textDecoration: 'none'
          })}
        >
          My Playlists
        </NavLink>

        <NavLink
          to="/favorite"
          style={({ isActive }) => ({
            color: isActive ? 'yellow' : 'white',
            textDecoration: 'none'
          })}
        >
          Favorites
        </NavLink>
      </div>

 
      <div>
        <input
          type="text"
          placeholder="Find a song..."
          style={{
            padding: '8px 12px',
            borderRadius: '8px',
            border: 'none',
            width: '250px'
          }}
        />
      </div>

 
      <div style={{ display: 'flex', gap: '20px', alignItems: 'center' }}>
        <NavLink
          to="/upload"
          style={({ isActive }) => ({
            color: isActive ? 'yellow' : 'white',
            textDecoration: 'none'
          })}
        >
          Upload Song
        </NavLink>
        <NavLink
          to="/profile"
          style={({ isActive }) => ({
            color: isActive ? 'yellow' : 'white',
            textDecoration: 'none'
          })}
        >
          Profile
        </NavLink>
      </div>
    </nav>
  );
}

export default Navbar;
