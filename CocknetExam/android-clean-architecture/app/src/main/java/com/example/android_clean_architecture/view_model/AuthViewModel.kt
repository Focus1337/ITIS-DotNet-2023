package com.example.android_clean_architecture.view_model

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.data.use_case.BackUseCaseImpl
import com.example.domain.dto.TokenData
import com.example.domain.use_case.BackUseCase
import kotlinx.coroutines.launch

class AuthViewModel : ViewModel() {
    private val myBookUseCase : BackUseCase = BackUseCaseImpl()
    val tokenDataMutable = MutableLiveData<TokenData?>()

    fun login(username: String, password: String) {
        viewModelScope.launch {
            tokenDataMutable.postValue(myBookUseCase.login(username, password))
        }
    }
}