
const API_BASE = 'https://localhost:7289/api';

// Users
export async function registerUser(name: string, phone: string) {
  const res = await fetch(`${API_BASE}/User`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ name, phone }),
  });
  if (!res.ok) throw new Error('Registration failed');
  return res.json();
}

export async function loginUser(phone: string) {
  const res = await fetch(`${API_BASE}/User/login`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ phone }),
  });
  if (!res.ok) throw new Error('Login failed');
  return res.json();
}

// Categories
export async function fetchCategories() {
  const res = await fetch(`${API_BASE}/Category`);
  if (!res.ok) throw new Error('Failed to fetch categories');
  return res.json();
}

// SubCategories
export async function fetchSubCategories(categoryId: number) {
  const res = await fetch(`${API_BASE}/SubCategory?categoryId=${categoryId}`);
  if (!res.ok) throw new Error('Failed to fetch sub-categories');
  return res.json();
}

// Prompts (lessons)
export async function sendPrompt(data: {
  userId: string;
  categoryId: number;
  subCategoryId: number;
  prompt: string;
}) {
  const res = await fetch(`${API_BASE}/Prompt`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to send prompt');
  return res.json();
}

// היסטוריית למידה
export async function fetchUserPrompts(userId: string) {
  const res = await fetch(`${API_BASE}/Prompt/user/${userId}`);
  if (!res.ok) throw new Error('Failed to fetch user prompts');
  return res.json();
}