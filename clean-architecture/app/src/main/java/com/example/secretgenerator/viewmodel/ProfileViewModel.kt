package com.example.secretgenerator.viewmodel

import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.data.usecase.UserUseCase
import com.example.domain.dto.UserData
import kotlinx.coroutines.launch

class ProfileViewModel : ViewModel() {

    private val userUseCase = UserUseCase()
    val userDataMutable = MutableLiveData<UserData?>()

    fun getUserData(id: Int) {
        viewModelScope.launch {
            userDataMutable.postValue(userUseCase.getUser(id))
        }
    }
}