import { Link, useNavigate } from 'react-router-dom';

const Home = () => {
  const navigate = useNavigate();

  const handleLogout = () => {
    // Atsijungimo logika (pvz., tokeno trynimas)
    navigate('/login');
  };

  return (
    <div style={{ position: 'relative', height: '100vh', width: '100vw' }}>
      {/* Viršutinė juosta */}
      <header
        style={{
          display: 'flex',
          justifyContent: 'space-between',
          alignItems: 'flex-start',
          padding: '1rem',
          position: 'absolute',
          top: 0,
          left: 0,
          right: 0,
        }}
      >
        {/* Tuščias blokas kairėje kad subalansuotų tarpą */}
        <div style={{ width: '100px' }}></div>

        {/* Viduryje - Coffee Shop */}
        <div style={{ textAlign: 'center', flexGrow: 1 }}>
          <Link to="/" style={{ fontSize: '1.5rem', fontWeight: 'bold', textDecoration: 'underline', color: 'black' }}>
            Coffee Shop
          </Link>
        </div>

        {/* Dešinėje - ikonėlės ir logout */}
        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '0.5rem' }}>
          <span style={{ fontSize: '1.5rem' }}>🛒</span>
          <span style={{ fontSize: '1.5rem' }}>👤</span>
          <button onClick={handleLogout}>Log out</button>
        </div>
      </header>
    </div>
  );
};

export default Home;
