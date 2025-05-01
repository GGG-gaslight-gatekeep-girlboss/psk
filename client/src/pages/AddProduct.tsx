import { useNavigate } from 'react-router-dom';
import { useState } from 'react';

const AddProduct = () => {
  const navigate = useNavigate();

  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [quantity, setQuantity] = useState('');
  const [image, setImage] = useState<File | null>(null);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    // Čia bus siuntimas į serverį arba pridėjimas į sąrašą
    console.log({ name, description, price, quantity, image });
    navigate('/products-management');
  };

  return (
    <div style={{ position: 'relative', height: '100vh', width: '100vw', paddingTop: '5rem' }}>
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
        {/* Kairysis tarpas */}
        <div style={{ width: '100px' }}></div>

        {/* Viduryje - Coffee Shop */}
        <div style={{ textAlign: 'center', flexGrow: 1 }}>
          <a
            href="/"
            style={{
              fontSize: '1.5rem',
              fontWeight: 'bold',
              textDecoration: 'underline',
              color: 'black',
            }}
          >
            Coffee Shop
          </a>
        </div>

        {/* Dešinėje - ikonėlės */}
        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', gap: '0.5rem' }}>
          <span style={{ fontSize: '1.5rem' }}>🛒</span>
          <span style={{ fontSize: '1.5rem' }}>👤</span>
        </div>
      </header>

      {/* Forma */}
      <div style={{ display: 'flex', justifyContent: 'center', marginTop: '2rem' }}>
        <form
          onSubmit={handleSubmit}
          style={{ display: 'flex', flexDirection: 'column', gap: '1rem', width: '300px' }}
        >
          <h2 style={{ textAlign: 'center' }}>Add product</h2>

          <label>
            Name
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              style={{ width: '100%', padding: '0.5rem' }}
              required
            />
          </label>

          <label>
            Description
            <input
              type="text"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              style={{ width: '100%', padding: '0.5rem' }}
              required
            />
          </label>

          <label>
            Price
            <input
              type="number"
              step="0.01"
              value={price}
              onChange={(e) => setPrice(e.target.value)}
              style={{ width: '100%', padding: '0.5rem' }}
              required
            />
          </label>

          <label>
            Quantity
            <input
              type="number"
              value={quantity}
              onChange={(e) => setQuantity(e.target.value)}
              style={{ width: '100%', padding: '0.5rem' }}
              required
            />
          </label>

          <label>
            Image
            <input
              type="file"
              onChange={(e) => setImage(e.target.files ? e.target.files[0] : null)}
              style={{ width: '100%', padding: '0.5rem' }}
              required
            />
          </label>

          <button
            type="submit"
            style={{
              backgroundColor: '#007bff',
              color: 'white',
              border: 'none',
              padding: '0.75rem',
              borderRadius: '5px',
              cursor: 'pointer',
              fontSize: '1rem',
            }}
          >
            Add product
          </button>
        </form>
      </div>
    </div>
  );
};

export default AddProduct;
