using System.Linq.Expressions;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Library.Utils;

namespace ESPlatform.QRCode.IMS.Infra.Builders;

public static class AccountPredicateBuilder {
	public static Expression<Func<TbNguoiDung, bool>> Init() {
		return PredicateBuilder.True<TbNguoiDung>();
	}

	public static Expression<Func<TbNguoiDung, bool>> HasId(this Expression<Func<TbNguoiDung, bool>> predicate, int accountId) {
		return predicate.And(x => x.MaNguoiDung == accountId);
	}

	public static Expression<Func<TbNguoiDung, bool>> HasUsername(this Expression<Func<TbNguoiDung, bool>> predicate, string username) {
		return predicate.And(x => x.TenDangNhap == username);
	}

	public static Expression<Func<TbNguoiDung, bool>> HasAnyStatus(this Expression<Func<TbNguoiDung, bool>> predicate, bool status) {
		return predicate.And(x => x.KichHoat == status);
	}

	public static Expression<Func<TbNguoiDung, bool>> isDelete(this Expression<Func<TbNguoiDung, bool>> predicate, bool isDelete) {
		return predicate.And(x => x.DaXoa == isDelete);
	}
}
