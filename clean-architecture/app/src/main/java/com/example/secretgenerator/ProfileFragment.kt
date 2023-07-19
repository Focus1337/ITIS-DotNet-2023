package com.example.secretgenerator

import android.content.Context
import android.content.Intent
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.lifecycle.ViewModelProvider
import com.example.secretgenerator.databinding.FragmentProfileBinding
import com.example.secretgenerator.viewmodel.ProfileViewModel
import kotlin.random.Random

class ProfileFragment : Fragment() {
    private var _binding: FragmentProfileBinding? = null
    private val binding get() = _binding!!
    private lateinit var viewModel: ProfileViewModel
    private lateinit var prefManager: PrefManager

    override fun onAttach(context: Context) {
        super.onAttach(context)
        prefManager = PrefManager(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentProfileBinding.inflate(inflater, container, false)
        viewModel = ViewModelProvider(this)[ProfileViewModel::class.java]

        binding.helloTextView.text = "Hello, ${prefManager.getUsername()}"

        viewModel.getUserData(Random.nextInt(40))
        viewModel.userDataMutable.observe(viewLifecycleOwner) {
            if (it == null) {
                binding.curUsernameTextInput.setText("Empty data")
                binding.curPasswordTextInput.setText("Empty data")
            } else {
                binding.curUsernameTextInput.setText(it.email)
                binding.curPasswordTextInput.setText(it.username)
            }
        }

        binding.deleteButton.setOnClickListener {
            deleteAccount()
        }

        binding.logoutButton.setOnClickListener {
            logout()
        }

        return binding.root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }

    private fun deleteAccount() {
        prefManager.removeData()
        openAuth()
    }

    private fun logout() {
        prefManager.setAuth(false)
        openAuth()
    }

    private fun openAuth() {
        val intent = Intent(activity, AuthActivity::class.java)
        startActivity(intent)
        activity?.finish()
    }
}