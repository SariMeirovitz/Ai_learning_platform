import { configureStore } from '@reduxjs/toolkit';
import userReducer from './userSlice';
import categoryReducer from './categorySlice'; // הוסף שורה זו
import promptReducer from './promptSlice';
import adminReducer from './adminSlice';

export const store = configureStore({
  reducer: {
    user: userReducer,
    category: categoryReducer,
    prompt: promptReducer,
    admin: adminReducer,
  },
});

// טיפוסי עזר לשימוש ב-TypeScript בפרויקט
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;