import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchCategoriesThunk, fetchSubCategoriesThunk } from '../redux/thunk';
import type { AppDispatch, RootState } from '../redux/store';
import type { Category, SubCategory } from '../redux/categorySlice';
import {
  Box,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  CircularProgress,
  Alert,
} from '@mui/material';

interface Props {
  categoryId: number | null;
  subCategoryId: number | null;
  onCategoryChange: (id: number | null) => void;
  onSubCategoryChange: (id: number | null) => void;
}

export default function CategorySelect({
  categoryId,
  subCategoryId,
  onCategoryChange,
  onSubCategoryChange,
}: Props) {
  const dispatch = useDispatch<AppDispatch>();
  const { categories, subCategories, loading, error } = useSelector(
    (state: RootState) => state.category
  );

  useEffect(() => {
    dispatch(fetchCategoriesThunk());
  }, [dispatch]);

  useEffect(() => {
    if (categoryId !== null) {
      dispatch(fetchSubCategoriesThunk(categoryId));
    }
  }, [categoryId, dispatch]);

  // סינון תתי־קטגוריות לפי הקטגוריה שנבחרה
  const filteredSubCategories = subCategories.filter(
    (sub: SubCategory) => sub.categoryId === categoryId
  );

  return (
    <Box
      sx={{
        display: 'flex',
        gap: 3,
        alignItems: 'center',
        direction: 'rtl',
        my: 2,
        flexWrap: 'wrap',
      }}
    >
      <FormControl sx={{ minWidth: 180 }} size="small">
        <InputLabel id="category-label">קטגוריה</InputLabel>
        <Select
          labelId="category-label"
          value={categoryId ?? ''}
          label="קטגוריה"
          onChange={e =>
            onCategoryChange(e.target.value ? Number(e.target.value) : null)
          }
          disabled={loading}
        >
          <MenuItem value="">
            <em>בחר קטגוריה</em>
          </MenuItem>
          {categories.map((cat: Category) => (
            <MenuItem key={cat.id} value={cat.id}>
              {cat.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
      <FormControl sx={{ minWidth: 180 }} size="small" disabled={!categoryId || loading}>
        <InputLabel id="sub-category-label">תת־קטגוריה</InputLabel>
        <Select
          labelId="sub-category-label"
          value={subCategoryId ?? ''}
          label="תת־קטגוריה"
          onChange={e =>
            onSubCategoryChange(e.target.value ? Number(e.target.value) : null)
          }
        >
          <MenuItem value="">
            <em>בחר תת־קטגוריה</em>
          </MenuItem>
          {filteredSubCategories.map((sub: SubCategory) => (
            <MenuItem key={sub.id} value={sub.id}>
              {sub.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
      {loading && <CircularProgress size={24} sx={{ mx: 2 }} />}
      {error && <Alert severity="error" sx={{ mx: 2 }}>{error}</Alert>}
    </Box>
  );
}