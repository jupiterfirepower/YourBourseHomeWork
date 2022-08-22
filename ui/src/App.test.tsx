import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders readme prompt', () => {
  render(<App />);
  const readmeElement = screen.getByText(/Please see the README at the root of the repository./i);
  expect(readmeElement).toBeInTheDocument();
});
