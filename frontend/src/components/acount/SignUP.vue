<template>
  <form @submit.prevent="onsubmits" class="from_signup">
    <div class="title">
      <h1>Đăng ký</h1>
    </div>
    <div class="parent_firtsname">
      <label>Tên</label>
      <input v-model="data_post.firtname" type="text" name="" id="" />
      <span v-if="err.firtname" class="err">{{ err.firtname }}</span>
    </div>
    <div class="parent_firtsname">
      <label>Họ</label>
      <input v-model="data_post.lastname" type="text" name="" id="" />
      <span v-if="err.lastname" class="err">{{ err.firtname }}</span>
    </div>
    <div class="parent_firtsname">
      <label>Ngày sinh</label>
      <input v-model="data_post.dob" type="Date" name="" id="" />
      <span v-if="err.dob" class="err">{{ err.dob }}</span>
    </div>
    <div class="parent_firtsname">
      <label for="">Email</label>
      <input v-model="data_post.email" type="text" name="" id="" />
      <span v-if="err.email" class="err">{{ err.email }}</span>
    </div>
    <div class="parent_firtsname">
      <label>điện thoại</label>
      <input v-model="data_post.phone" type="text" name="" id="" />
      <span v-if="err.phone" class="err">{{ err.phone }}</span>
    </div>
    <div class="parent_firtsname">
      <label>Mật khẩu</label>
      <input v-model="data_post.password" type="password" name="" id="" />
      <span v-if="err.password" class="err">{{ err.password }}</span>
    </div>
    <div>
      <div class="btn">
        <button type="submit">Đăng ký</button>
        <p
          @click="
            this.$router.push({ name: 'acount', query: { action: 'login' } })
          "
        >
          Đã có tài khoản
        </p>
      </div>
    </div>
  </form>
</template>
<script>
import { reactive } from "vue";
import store from "@/store";
export default {
  setup() {
    store.dispatch("acount/get_all");
    const data_post = reactive({
      firtname: null,
      lastname: null,
      dob: null,
      email: null,
      phone: null,
      password: null,
      role: "user",
      token: null,
    });
    const err = reactive({
      firtname: null,
      lastname: null,
      dob: null,
      email: null,
      phone: null,
      password: null,
    });
    const regexp_pasword = /^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$/;
    const regexp_email =
      /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    const regex_phone = /^[0-9\-+]{9,15}$/;
    const onsubmits = () => {
      data_post.firtname == null
        ? (err.firtname = "Không bỏ trống tên")
        : (err.firtname = null);
      data_post.lastname == null
        ? (err.lastname = "Không bỏ trống họ")
        : (err.lastname = null);
      data_post.dob == null
        ? (err.dob = "Không bỏ trống ngày sinh")
        : new Date(data_post.dob).getTime() > new Date().getTime()
        ? (err.dob = "Ngày sinh không hợp lệ")
        : (err.dob = null);
      data_post.email == null
        ? (err.email = "Không bỏ trống email")
        : regexp_email.test(data_post.email) == false
        ? (err.email = "email không đúng định dạng")
        : (err.email = null);
      data_post.phone == null
        ? (err.phone = "không bỏ trống")
        : regex_phone.test(data_post.phone) == false
        ? (err.phone = "số không đúng định dạng")
        : (err.phone = null);
      data_post.password == null
        ? (err.password = "không bỏ trống mật khẩu")
        : regexp_pasword.test(data_post.password) == false
        ? (err.password = "mật khẩu không đúng định dạng")
        : (err.password = null);
      if (
        err.dob == null &&
        err.email == null &&
        err.firtname == null &&
        err.lastname == null &&
        err.password == null &&
        err.phone == null
      ) {
        data_post.token = data_post.email + data_post.phone;
        // check exist
        var check = true;
        if (store.state.acount.emloyee != null) {
          for (var item in store.state.acount.emloyee) {
            if (store.state.acount.emloyee[item].email == data_post.email) {
              check = false;
              err.email = "email đã được sử dụng";
            }
          }
        }
        if (check == true) {
          store.dispatch("acount/register", data_post);
        }
      }
    };
    return { onsubmits, data_post, err };
  },
};
</script>
<style scoped>
.err {
  color: red;
  font-size: 15px;
  position: absolute;
  top: calc(100% + 5px);
  left: 50%;
  transform: translateX(-50%);
}
.btn {
  padding: 20px;
  text-align: center;
}
.btn > button {
  padding: 10px 30px;
  background: var(--color_main);
  color: white;
  border-radius: 5px;
}
.from_signup {
  background: white;
  padding: 10px;
  border-radius: 10px;
  width: 500px;
}
.from_signup > div:not(.btn) {
  display: flex;
  flex-direction: column;
  text-align: start;
  margin: 0 30px;
  margin-bottom: 10px;
  gap: 5px;
  position: relative;
}
.from_signup > div > input {
  height: 49px;
  padding-left: 10px;
  flex-shrink: 0;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}
</style>
