import { useEffect } from 'react';
import type { AppDispatch } from '../redux/store'; // הוסיפי type כאן!
import { useDispatch, useSelector } from 'react-redux';
import { fetchAllUsersWithPromptsThunk } from '../redux/thunk';

export default function AdminDashboard() {
const dispatch = useDispatch<AppDispatch>();  const { users, loading, error } = useSelector((state: any) => state.admin);

  useEffect(() => {
    dispatch(fetchAllUsersWithPromptsThunk());
  }, [dispatch]);

  if (loading) return <div>טוען...</div>;
  if (error) return <div style={{ color: 'red' }}>שגיאה: {error}</div>;

  return (
    <div style={{ maxWidth: 900, margin: '40px auto' }}>
      <h2>ניהול משתמשים</h2>
      <table style={{ width: '100%', borderCollapse: 'collapse' }}>
        <thead>
          <tr>
            <th>שם</th>
            <th>טלפון</th>
            <th>היסטוריית למידה</th>
          </tr>
        </thead>
        <tbody>
          {users.map((u: any, userIdx: number) => (
            <tr key={u.id ?? userIdx}>
              <td>{u.name}</td>
              <td>{u.phone}</td>
              <td>
                {(u.prompts && u.prompts.length > 0) ? (
                  <ul>
                    {u.prompts.map((p: any, promptIdx: number) => (
                      <li key={`user${userIdx}-prompt${promptIdx}`}>
                        <b>שאלה:</b> {p.prompt} <br />
                        <b>תשובה:</b> {p.response}
                      </li>
                    ))}
                  </ul>
                ) : (
                  <span style={{ color: '#888' }}>אין היסטוריה</span>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}