const API_BASE = 'https://localhost:7289/api';

// פונקציה לבניית headers תקינים לכל קריאה
function buildHeaders(): Record<string, string> {
  const token = localStorage.getItem('token');
  const headers: Record<string, string> = {
    'Content-Type': 'application/json',
  };
  if (token) {
    headers['Authorization'] = `Bearer ${token}`;
  }
  return headers;
}

// Users
export async function registerUser(name: string, phone: string) {
  const res = await fetch(`${API_BASE}/User`, {
    method: 'POST',
    headers: buildHeaders(),
    body: JSON.stringify({ name, phone }),
  });
  if (!res.ok) throw new Error('Registration failed');
  return res.json();
}

export async function loginUser(name: string, phone: string) {
  const res = await fetch(`${API_BASE}/Auth/login`, {
    method: 'POST',
    headers: buildHeaders(),
    body: JSON.stringify({ name, phone }),
  });
  if (!res.ok) throw new Error('Login failed');
  return res.json(); // מחזיר { token, user }
}

// Categories
export async function fetchCategories() {
  const res = await fetch(`${API_BASE}/Category`, {
    headers: buildHeaders(),
  });
  if (!res.ok) throw new Error('Failed to fetch categories');
  return res.json();
}

// SubCategories
export async function fetchSubCategories(categoryId: number) {
  const res = await fetch(`${API_BASE}/SubCategory?categoryId=${categoryId}`, {
    headers: buildHeaders(),
  });
  if (!res.ok) throw new Error('Failed to fetch sub-categories');
  return res.json();
}

// Prompts (lessons)
export async function sendPrompt(data: {
  categoryId: number;
  subCategoryId: number;
  prompt1: string;
}) {
  const res = await fetch(`${API_BASE}/Prompt/submit`, {
    method: 'POST',
    headers: buildHeaders(),
    body: JSON.stringify(data),
  });
  if (!res.ok) throw new Error('Failed to send prompt');
  return res.json();
}


// קריאה לאדמין - כל המשתמשים עם היסטוריה
export async function fetchAllUsersWithPrompts() {
  const res = await fetch(`${API_BASE}/Admin/all-users-with-prompts`, {
    headers: buildHeaders(),
  });
  if (!res.ok) throw new Error('אין הרשאה או שהמשתמש לא קיים');
  return res.json();
}

// היסטוריית למידה של המשתמש המחובר
export async function fetchMyHistory() {
  const res = await fetch(`${API_BASE}/Prompt/my-history`, {
    headers: buildHeaders(),
  });
  if (!res.ok) throw new Error('Failed to fetch my history');
  return res.json();
}