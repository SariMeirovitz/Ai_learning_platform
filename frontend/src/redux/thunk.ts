import { createAsyncThunk } from '@reduxjs/toolkit';
import {
  registerUser,
  fetchCategories,
  fetchSubCategories,
  sendPrompt,
  fetchUserPrompts,
  loginUser,
} from '../services/api';
import type { User } from './userSlice';

// הרשמה
export const registerUserThunk = createAsyncThunk<User, { name: string; phone: string }>(
  'user/register',
  async ({ name, phone }, { rejectWithValue }) => {
    try {
      return await registerUser(name, phone);
    } catch (err: any) {
      return rejectWithValue(err.message || 'Registration failed');
    }
  }
);
//התחברות
export const loginUserThunk = createAsyncThunk<User, { phone: string }>(
  'user/login',
  async ({ phone }, { rejectWithValue }) => {
    try {
      return await loginUser(phone);
    } catch (err: any) {
      return rejectWithValue(err.message || 'Login failed');
    }
  }
);

// שליפת קטגוריות
export const fetchCategoriesThunk = createAsyncThunk(
  'categories/fetchAll',
  async (_, { rejectWithValue }) => {
    try {
      return await fetchCategories();
    } catch (err: any) {
      return rejectWithValue(err.message || 'Failed to fetch categories');
    }
  }
);

// שליפת תתי־קטגוריות
export const fetchSubCategoriesThunk = createAsyncThunk(
  'categories/fetchSubs',
  async (categoryId: number, { rejectWithValue }) => {
    try {
      return await fetchSubCategories(categoryId);
    } catch (err: any) {
      return rejectWithValue(err.message || 'Failed to fetch sub-categories');
    }
  }
);

// שליחת prompt
export const sendPromptThunk = createAsyncThunk(
  'prompts/send',
  async (data: { userId: string; categoryId: number; subCategoryId: number; prompt: string }, { rejectWithValue }) => {
    try {
      return await sendPrompt(data);
    } catch (err: any) {
      return rejectWithValue(err.message || 'Failed to send prompt');
    }
  }
);

// שליפת היסטוריית למידה
export const fetchUserPromptsThunk = createAsyncThunk(
  'prompts/fetchUser',
  async (userId: string, { rejectWithValue }) => {
    try {
      return await fetchUserPrompts(userId);
    } catch (err: any) {
      return rejectWithValue(err.message || 'Failed to fetch user prompts');
    }
  }
);