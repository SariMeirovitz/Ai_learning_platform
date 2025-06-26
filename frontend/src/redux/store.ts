import { configureStore } from '@reduxjs/toolkit';
import userReducer from './userSlice';
import categoryReducer from './categorySlice'; // הוסף שורה זו
import promptReducer from './promptSlice';

export const store = configureStore({
  reducer: {
    user: userReducer,
    category: categoryReducer,
    prompt: promptReducer,
  },
});

// טיפוסי עזר לשימוש ב-TypeScript בפרויקט
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;