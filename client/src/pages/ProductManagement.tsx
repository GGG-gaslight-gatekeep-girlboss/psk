import { Link, useNavigate } from 'react-router-dom';

const products = [
  { id: 1, name: 'Espresso', price: 3.99, image: 'https://via.placeholder.com/100' },
  { id: 2, name: 'Latte', price: 2.99, image: 'https://via.placeholder.com/100' },
  { id: 3, name: 'Cappuccino', price: 4.99, image: 'https://via.placeholder.com/100' },
  { id: 4, name: 'Mocha', price: 3.99, image: 'https://via.placeholder.com/100' },
  { id: 5, name: 'Americano', price: 2.49, image: 'https://via.placeholder.com/100' },
];

const ProductManagement = () => {
  const navigate = useNavigate();

  return (
    <div style={{ position: 'relative', minHeight: '100vh', width: '100vw', paddingTop: '5rem' }}>
      {/* Header */}
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
        <div style={{ width: '100px' }}></div>

        <div style={{ textAlign: 'center', flexGrow: 1 }}>
          <Link to="/" style={{ fontSize: '1.5rem', fontWeight: 'bold', textDecoration: 'underline', color: 'black' }}>
            Coffee Shop
          </Link>
        </div>

        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '0.5rem' }}>
            <span style={{ fontSize: '1.5rem' }}>🛒</span>
            <span style={{ fontSize: '1.5rem' }}>👤</span>
            <button
                onClick={() => navigate('/add-product')}
                style={{
                    backgroundColor: '#007bff',
                    color: 'white',
                    border: 'none',
                    padding: '0.5rem 1rem',
                    borderRadius: '5px',
                    cursor: 'pointer',
            }}
            >
                Add product
            </button>
        </div>

      </header>

      <main style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', padding: '1rem' }}>
        <div
          style={{
            display: 'grid',
            gridTemplateColumns: 'repeat(auto-fit, minmax(150px, 1fr))',
            gap: '1.5rem',
            width: '80%',
            justifyItems: 'center',
          }}
        >
          {products.map((product) => (
            <div
              key={product.id}
              style={{
                border: '1px solid #ccc',
                padding: '0.5rem',
                textAlign: 'center',
                width: '150px',
              }}
            >
              <img src={product.image} alt={product.name} style={{ width: '100px', height: '100px' }} />
              <div style={{ marginTop: '0.5rem' }}>{product.name}</div>
              <div style={{ fontWeight: 'bold' }}>{product.price.toFixed(2)}€</div>
            </div>
          ))}
        </div>
      </main>
    </div>
  );
};

export default ProductManagement;
